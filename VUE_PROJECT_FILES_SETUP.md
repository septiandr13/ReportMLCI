# Vue Project API Configuration Files

## File 1: env.local (Root Project Vue)

```
VITE_API_BASE_URL=http://localhost:5000
VITE_REPORT_ENDPOINT=/api/report
VITE_API_TIMEOUT=30000
```

---

## File 2: src/config/api.config.js

```javascript
/**
 * API Configuration untuk Report Service
 */

export const apiConfig = {
  // Base URL untuk API
  baseURL: import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000',

  // Timeout (milliseconds)
  timeout: parseInt(import.meta.env.VITE_API_TIMEOUT) || 30000,

  // Headers default
  headers: {
    'Content-Type': 'application/json',
  },

  // Endpoints
  endpoints: {
    report: {
      base: import.meta.env.VITE_REPORT_ENDPOINT || '/api/report',
      getAllClauses: () => '/api/report/clause-mlci',
      getClauseById: (id) => `/api/report/clause-mlci/${id}`,
    }
  },

  // Retry configuration
  retry: {
    maxAttempts: 3,
    delay: 1000, // milliseconds
    backoff: 2, // multiplier
  },

  // Cache configuration
  cache: {
    enabled: true,
    duration: 5 * 60 * 1000, // 5 minutes
    key: 'report_cache',
  }
};

export default apiConfig;
```

---

## File 3: src/services/httpClient.js

```javascript
/**
 * HTTP Client dengan Axios
 * Handles requests, responses, dan error management
 */

import axios from 'axios';
import apiConfig from '@/config/api.config';

class HttpClient {
  constructor(config = apiConfig) {
    this.config = config;
    this.client = axios.create({
      baseURL: config.baseURL,
      timeout: config.timeout,
      headers: config.headers,
    });

    // Setup interceptors
    this.setupInterceptors();
  }

  setupInterceptors() {
    // Request interceptor
    this.client.interceptors.request.use(
      (config) => {
        // Add auth token jika ada
        const token = localStorage.getItem('auth_token');
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }

        console.log('[HTTP] Request:', config.method.toUpperCase(), config.url);
        return config;
      },
      (error) => {
        console.error('[HTTP] Request Error:', error);
        return Promise.reject(error);
      }
    );

    // Response interceptor
    this.client.interceptors.response.use(
      (response) => {
        console.log('[HTTP] Response:', response.status, response.config.url);
        return response;
      },
      (error) => {
        console.error('[HTTP] Response Error:', error);

        // Handle specific status codes
        if (error.response?.status === 401) {
          // Unauthorized - redirect to login
          localStorage.removeItem('auth_token');
          window.location.href = '/login';
        }

        return Promise.reject(error);
      }
    );
  }

  async get(url, config = {}) {
    return this.client.get(url, config);
  }

  async post(url, data, config = {}) {
    return this.client.post(url, data, config);
  }

  async put(url, data, config = {}) {
    return this.client.put(url, data, config);
  }

  async delete(url, config = {}) {
    return this.client.delete(url, config);
  }

  async request(config) {
    return this.client.request(config);
  }
}

export const httpClient = new HttpClient(apiConfig);
export default HttpClient;
```

---

## File 4: src/services/cache.service.js

```javascript
/**
 * Cache Service untuk menyimpan response
 * Mengurangi API calls dan meningkatkan performance
 */

import apiConfig from '@/config/api.config';

class CacheService {
  constructor() {
    this.memoryCache = new Map();
    this.config = apiConfig.cache;
  }

  /**
   * Generate cache key dari URL
   */
  generateKey(url) {
    return `${this.config.key}_${url}`;
  }

  /**
   * Get data dari cache
   */
  get(key) {
    const cached = this.memoryCache.get(key);

    if (!cached) {
      return null;
    }

    // Check if expired
    if (Date.now() - cached.timestamp > this.config.duration) {
      this.memoryCache.delete(key);
      return null;
    }

    return cached.data;
  }

  /**
   * Set data ke cache
   */
  set(key, data) {
    this.memoryCache.set(key, {
      data,
      timestamp: Date.now()
    });
  }

  /**
   * Clear specific cache
   */
  clear(key) {
    this.memoryCache.delete(key);
  }

  /**
   * Clear all cache
   */
  clearAll() {
    this.memoryCache.clear();
  }

  /**
   * Get cache stats
   */
  getStats() {
    return {
      size: this.memoryCache.size,
      duration: this.config.duration,
      enabled: this.config.enabled
    };
  }
}

export const cacheService = new CacheService();
export default CacheService;
```

---

## File 5: src/services/reportService.js (Enhanced)

```javascript
/**
 * Report Service dengan caching dan retry logic
 */

import { httpClient } from './httpClient';
import { cacheService } from './cache.service';
import apiConfig from '@/config/api.config';

class ReportService {
  constructor() {
    this.config = apiConfig;
  }

  /**
   * Retry logic untuk failed requests
   */
  async retryRequest(requestFn, retries = this.config.retry.maxAttempts) {
    let lastError;

    for (let attempt = 1; attempt <= retries; attempt++) {
      try {
        return await requestFn();
      } catch (error) {
        lastError = error;

        if (attempt < retries) {
          const delay = this.config.retry.delay * Math.pow(
            this.config.retry.backoff,
            attempt - 1
          );
          console.warn(
            `[ReportService] Retry attempt ${attempt}/${retries} after ${delay}ms`
          );
          await new Promise(resolve => setTimeout(resolve, delay));
        }
      }
    }

    throw lastError;
  }

  /**
   * Get semua report data
   */
  async getAllReports(useCache = true) {
    const cacheKey = cacheService.generateKey('all-reports');

    // Check cache
    if (useCache && this.config.cache.enabled) {
      const cached = cacheService.get(cacheKey);
      if (cached) {
        console.log('[ReportService] Using cached data');
        return cached;
      }
    }

    try {
      const response = await this.retryRequest(() =>
        httpClient.get(this.config.endpoints.report.getAllClauses())
      );

      if (response.data.success) {
        // Cache the response
        if (this.config.cache.enabled) {
          cacheService.set(cacheKey, response.data);
        }
        return response.data;
      } else {
        throw new Error(response.data.message || 'Failed to fetch reports');
      }
    } catch (error) {
      console.error('[ReportService] Get all reports error:', error);
      throw error;
    }
  }

  /**
   * Get report by ID
   */
  async getReportById(id, useCache = true) {
    const cacheKey = cacheService.generateKey(`report-${id}`);

    // Check cache
    if (useCache && this.config.cache.enabled) {
      const cached = cacheService.get(cacheKey);
      if (cached) {
        console.log('[ReportService] Using cached data for ID:', id);
        return cached;
      }
    }

    try {
      const response = await this.retryRequest(() =>
        httpClient.get(this.config.endpoints.report.getClauseById(id))
      );

      if (response.data.success) {
        // Cache the response
        if (this.config.cache.enabled) {
          cacheService.set(cacheKey, response.data);
        }
        return response.data;
      } else {
        throw new Error(response.data.message || 'Report not found');
      }
    } catch (error) {
      console.error(`[ReportService] Get report by ID error (${id}):`, error);
      throw error;
    }
  }

  /**
   * Search clause dalam data
   */
  searchClauses(data, searchTerm) {
    if (!searchTerm || searchTerm.trim() === '') {
      return data;
    }

    const term = searchTerm.toLowerCase();

    return data
      .map(header => ({
        ...header,
        clauses: header.clauses
          .map(clause => ({
            ...clause,
            details: clause.details
              .map(detail => ({
                ...detail,
                subDetails: detail.subDetails?.filter(sub =>
                  sub.subDetailCode?.toLowerCase().includes(term) ||
                  sub.subDetailTitle?.toLowerCase().includes(term) ||
                  sub.subDetailContent?.toLowerCase().includes(term)
                ) || []
              }))
              .filter(detail =>
                detail.subDetails.length > 0 ||
                detail.clauseSubCode?.toLowerCase().includes(term) ||
                detail.clauseSubTitle?.toLowerCase().includes(term)
              )
          }))
          .filter(clause =>
            clause.details.length > 0 ||
            clause.clauseCode?.toLowerCase().includes(term) ||
            clause.clauseTitle?.toLowerCase().includes(term) ||
            clause.clauseContent?.toLowerCase().includes(term)
          )
      }))
      .filter(header =>
        header.clauses.length > 0 ||
        header.clauseHeaderCode?.toLowerCase().includes(term) ||
        header.clauseHeaderTitle?.toLowerCase().includes(term)
      );
  }

  /**
   * Filter active items
   */
  filterActive(data) {
    return data
      .filter(header => header.isActive)
      .map(header => ({
        ...header,
        clauses: header.clauses
          .filter(clause => clause.isActive)
          .map(clause => ({
            ...clause,
            details: clause.details
              .filter(detail => detail.isActive)
              .map(detail => ({
                ...detail,
                subDetails: detail.subDetails?.filter(sub => sub.isActive) || []
              }))
          }))
      }));
  }

  /**
   * Transform ke flat table format
   */
  flattenForTable(data) {
    const flattened = [];

    data.forEach(header => {
      header.clauses?.forEach(clause => {
        clause.details?.forEach(detail => {
          detail.subDetails?.forEach(subDetail => {
            flattened.push({
              headerId: header.id,
              headerCode: header.clauseHeaderCode,
              headerTitle: header.clauseHeaderTitle,
              headerDescription: header.clauseHeaderDescription,
              clauseId: clause.id,
              clauseCode: clause.clauseCode,
              clauseTitle: clause.clauseTitle,
              clauseContent: clause.clauseContent,
              detailId: detail.id,
              detailCode: detail.clauseSubCode,
              detailTitle: detail.clauseSubTitle,
              detailContent: detail.clauseSubContent,
              subDetailId: subDetail.id,
              subDetailCode: subDetail.subDetailCode,
              subDetailTitle: subDetail.subDetailTitle,
              subDetailContent: subDetail.subDetailContent,
              isActive: subDetail.isActive
            });
          });
        });
      });
    });

    return flattened;
  }

  /**
   * Clear cache
   */
  clearCache() {
    cacheService.clearAll();
    console.log('[ReportService] Cache cleared');
  }

  /**
   * Get cache statistics
   */
  getCacheStats() {
    return cacheService.getStats();
  }
}

export const reportService = new ReportService();
export default ReportService;
```

---

## File 6: src/composables/useReport.js

```javascript
/**
 * Vue 3 Composable untuk Report functionality
 */

import { ref, computed, onMounted } from 'vue';
import { reportService } from '@/services/reportService';

export const useReport = () => {
  const reports = ref([]);
  const loading = ref(false);
  const error = ref(null);
  const searchTerm = ref('');
  const filterActive = ref(true);

  /**
   * Fetch semua reports
   */
  const fetchReports = async () => {
    loading.value = true;
    error.value = null;

    try {
      const response = await reportService.getAllReports();

      if (response.success) {
        reports.value = response.data;
      } else {
        error.value = response.message;
      }
    } catch (err) {
      error.value = err.message || 'Failed to fetch reports';
      console.error('Fetch reports error:', err);
    } finally {
      loading.value = false;
    }
  };

  /**
   * Fetch report by ID
   */
  const fetchReportById = async (id) => {
    loading.value = true;
    error.value = null;

    try {
      const response = await reportService.getReportById(id);

      if (response.success) {
        reports.value = [response.data];
      } else {
        error.value = response.message;
      }
    } catch (err) {
      error.value = err.message || 'Failed to fetch report';
      console.error('Fetch report by ID error:', err);
    } finally {
      loading.value = false;
    }
  };

  /**
   * Computed filtered reports
   */
  const filteredReports = computed(() => {
    let result = reports.value;

    // Apply active filter
    if (filterActive.value) {
      result = reportService.filterActive(result);
    }

    // Apply search filter
    if (searchTerm.value) {
      result = reportService.searchClauses(result, searchTerm.value);
    }

    return result;
  });

  /**
   * Get flat table data
   */
  const tableData = computed(() => {
    return reportService.flattenForTable(filteredReports.value);
  });

  /**
   * Clear cache
   */
  const clearCache = () => {
    reportService.clearCache();
  };

  /**
   * Get cache stats
   */
  const getCacheStats = () => {
    return reportService.getCacheStats();
  };

  return {
    // State
    reports,
    loading,
    error,
    searchTerm,
    filterActive,

    // Methods
    fetchReports,
    fetchReportById,
    clearCache,
    getCacheStats,

    // Computed
    filteredReports,
    tableData,
  };
};

export default useReport;
```

---

## File 7: src/stores/reportStore.js (Pinia)

```javascript
/**
 * Pinia Store untuk Report State Management
 */

import { defineStore } from 'pinia';
import { ref, computed } from 'vue';
import { reportService } from '@/services/reportService';

export const useReportStore = defineStore('report', () => {
  // State
  const reports = ref([]);
  const loading = ref(false);
  const error = ref(null);
  const searchTerm = ref('');
  const selectedReportId = ref(null);

  // Computed
  const filteredReports = computed(() => {
    if (!searchTerm.value) {
      return reports.value;
    }
    return reportService.searchClauses(reports.value, searchTerm.value);
  });

  const selectedReport = computed(() => {
    if (!selectedReportId.value) return null;
    return reports.value.find(r => r.id === selectedReportId.value);
  });

  const totalReports = computed(() => reports.value.length);

  // Actions
  const fetchReports = async () => {
    loading.value = true;
    error.value = null;

    try {
      const response = await reportService.getAllReports();
      if (response.success) {
        reports.value = response.data;
      } else {
        error.value = response.message;
      }
    } catch (err) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  };

  const fetchReportById = async (id) => {
    loading.value = true;
    error.value = null;

    try {
      const response = await reportService.getReportById(id);
      if (response.success) {
        selectedReportId.value = id;
      } else {
        error.value = response.message;
      }
    } catch (err) {
      error.value = err.message;
    } finally {
      loading.value = false;
    }
  };

  const setSearchTerm = (term) => {
    searchTerm.value = term;
  };

  const clearSearch = () => {
    searchTerm.value = '';
  };

  const selectReport = (id) => {
    selectedReportId.value = id;
  };

  const clearCache = () => {
    reportService.clearCache();
  };

  return {
    // State
    reports,
    loading,
    error,
    searchTerm,
    selectedReportId,

    // Computed
    filteredReports,
    selectedReport,
    totalReports,

    // Actions
    fetchReports,
    fetchReportById,
    setSearchTerm,
    clearSearch,
    selectReport,
    clearCache,
  };
});
```

---

## Installation Instructions

### Step 1: Copy all files to your Vue project

```
your-vue-project/
??? .env.local (buat file baru)
??? src/
?   ??? config/
?   ?   ??? api.config.js (buat folder & file baru)
?   ??? services/
?   ?   ??? httpClient.js (buat file baru)
?   ?   ??? cache.service.js (buat file baru)
?   ?   ??? reportService.js (update/buat file)
?   ??? composables/
?   ?   ??? useReport.js (buat folder & file baru)
?   ??? stores/
?   ?   ??? reportStore.js (buat file baru - jika pakai Pinia)
?   ??? components/
?       ??? ReportViewer.vue (gunakan dari guide)
```

### Step 2: Install dependencies

```bash
npm install axios
npm install pinia # optional, jika pakai Pinia
```

### Step 3: Setup dalam main.js

```javascript
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.mount('#app')
```

---
