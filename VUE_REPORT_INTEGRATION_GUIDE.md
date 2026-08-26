# Vue Report Integration Guide
## Panduan Integrasi Report MLCI ke Project Vue

---

## ?? Table of Contents
1. [Overview](#overview)
2. [API Endpoints](#api-endpoints)
3. [Response Format](#response-format)
4. [Installation](#installation)
5. [Usage Examples](#usage-examples)
6. [Component Examples](#component-examples)
7. [Error Handling](#error-handling)
8. [Troubleshooting](#troubleshooting)

---

## Overview

API Report Service telah diintegrasikan ke dalam project .NET 8 dan siap di-consume oleh aplikasi Vue Anda. API ini menyediakan data hierarchical report untuk Clause MLCI dengan struktur:

```
MasterHeaderClause (Header)
??? MasterClause (Clauses)
?   ??? MasterClausesDetails (Sub Details)
?   ?   ??? MasterClausesSubDetails (Sub-Sub Details)
```

---

## API Endpoints

### 1. Get All Report Data
**Endpoint:** `GET /api/report/clause-mlci`

Mengambil semua data report yang aktif dengan semua level detail.

**Example Request:**
```bash
curl -X GET "http://localhost:5000/api/report/clause-mlci" \
  -H "Content-Type: application/json"
```

**cURL untuk testing:**
```bash
curl http://localhost:5000/api/report/clause-mlci
```

---

### 2. Get Report by ID
**Endpoint:** `GET /api/report/clause-mlci/{id}`

Mengambil data report tertentu berdasarkan Header ID.

**Parameters:**
- `id` (Guid) - ID dari MasterHeaderClause

**Example Request:**
```bash
curl -X GET "http://localhost:5000/api/report/clause-mlci/550e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json"
```

---

## Response Format

### Success Response (200 OK)

```json
{
  "success": true,
  "message": "Report data retrieved successfully",
  "data": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440000",
      "clauseHeaderCode": "MLCI-001",
      "clauseHeaderTitle": "Main Clause Header",
      "clauseHeaderDescription": "Description of the clause",
      "isActive": true,
      "clauses": [
        {
          "id": "550e8400-e29b-41d4-a716-446655440001",
          "clauseCode": "C001",
          "clauseTitle": "Clause Title",
          "clauseContent": "Full content of the clause...",
          "isActive": true,
          "details": [
            {
              "id": "550e8400-e29b-41d4-a716-446655440002",
              "clauseSubCode": "SUB001",
              "clauseSubTitle": "Sub Clause Title",
              "clauseSubContent": "Sub clause content...",
              "isActive": true,
              "subDetails": [
                {
                  "id": "550e8400-e29b-41d4-a716-446655440003",
                  "subDetailCode": "SUBSUB001",
                  "subDetailTitle": "Sub Detail Title",
                  "subDetailContent": "Sub detail content...",
                  "isActive": true
                }
              ]
            }
          ]
        }
      ]
    }
  ]
}
```

### Error Response (404 Not Found)

```json
{
  "success": false,
  "message": "Report not found"
}
```

### Error Response (500 Server Error)

```json
{
  "success": false,
  "message": "Error retrieving report data",
  "error": "Exception message details"
}
```

---

## Installation

### 1. Install Required Dependencies

**Using npm:**
```bash
npm install axios
# atau jika menggunakan fetch API (sudah built-in di browser)
```

**Untuk Vue Report Viewer (opsional):**
```bash
npm install vue-report-viewer
# atau library report viewer lainnya sesuai pilihan Anda
```

### 2. Environment Configuration

Create file `.env.local` di root project Vue:

```env
VITE_API_BASE_URL=http://localhost:5000
VITE_REPORT_ENDPOINT=/api/report
```

Di `vite.config.js` atau config file Anda:

```javascript
export default {
  define: {
    __API_URL__: JSON.stringify(process.env.VITE_API_BASE_URL),
    __REPORT_ENDPOINT__: JSON.stringify(process.env.VITE_REPORT_ENDPOINT),
  },
}
```

---

## Usage Examples

### Using Axios

#### 1. Create API Service

**File: `src/services/reportService.js`**

```javascript
import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000';
const REPORT_ENDPOINT = import.meta.env.VITE_REPORT_ENDPOINT || '/api/report';

const reportService = {
  // Get all report data
  getAllReports: async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}${REPORT_ENDPOINT}/clause-mlci`);
      return response.data;
    } catch (error) {
      console.error('Error fetching all reports:', error);
      throw error;
    }
  },

  // Get report by ID
  getReportById: async (id) => {
    try {
      const response = await axios.get(
        `${API_BASE_URL}${REPORT_ENDPOINT}/clause-mlci/${id}`
      );
      return response.data;
    } catch (error) {
      console.error(`Error fetching report ${id}:`, error);
      throw error;
    }
  },

  // Search clause by code or title
  searchClauses: (data, searchTerm) => {
    const results = [];

    data.forEach(header => {
      header.clauses?.forEach(clause => {
        if (
          clause.clauseCode.toLowerCase().includes(searchTerm.toLowerCase()) ||
          clause.clauseTitle.toLowerCase().includes(searchTerm.toLowerCase())
        ) {
          results.push({
            ...clause,
            headerCode: header.clauseHeaderCode,
            headerId: header.id
          });
        }
      });
    });

    return results;
  },

  // Filter active items
  filterActive: (data) => {
    return data.filter(item => item.isActive === true);
  },

  // Transform flat data for table display
  flattenForTable: (data) => {
    const flattened = [];

    data.forEach(header => {
      header.clauses?.forEach(clause => {
        clause.details?.forEach(detail => {
          detail.subDetails?.forEach(subDetail => {
            flattened.push({
              headerId: header.id,
              headerCode: header.clauseHeaderCode,
              headerTitle: header.clauseHeaderTitle,
              clauseId: clause.id,
              clauseCode: clause.clauseCode,
              clauseTitle: clause.clauseTitle,
              detailId: detail.id,
              detailCode: detail.clauseSubCode,
              detailTitle: detail.clauseSubTitle,
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
};

export default reportService;
```

#### 2. Use in Vue Component

**File: `src/components/ReportViewer.vue`**

```vue
<template>
  <div class="report-viewer">
    <h1>Report Viewer - MLCI Clauses</h1>

    <!-- Loading State -->
    <div v-if="loading" class="loading">
      <p>Loading report data...</p>
    </div>

    <!-- Error State -->
    <div v-else-if="error" class="error">
      <p>Error: {{ error }}</p>
      <button @click="fetchReports">Retry</button>
    </div>

    <!-- Success State -->
    <div v-else class="content">
      <!-- Search Box -->
      <div class="search-section">
        <input 
          v-model="searchTerm" 
          type="text" 
          placeholder="Search clause code or title..."
          class="search-input"
        />
      </div>

      <!-- Report List -->
      <div class="report-list">
        <div 
          v-for="header in filteredReports" 
          :key="header.id" 
          class="report-header"
        >
          <h2>{{ header.clauseHeaderCode }} - {{ header.clauseHeaderTitle }}</h2>
          <p class="description">{{ header.clauseHeaderDescription }}</p>

          <!-- Clauses -->
          <div class="clauses">
            <div 
              v-for="clause in header.clauses" 
              :key="clause.id" 
              class="clause-item"
            >
              <h3>{{ clause.clauseCode }} - {{ clause.clauseTitle }}</h3>
              <p class="content">{{ clause.clauseContent }}</p>

              <!-- Details -->
              <div class="details">
                <div 
                  v-for="detail in clause.details" 
                  :key="detail.id" 
                  class="detail-item"
                >
                  <h4>{{ detail.clauseSubCode }} - {{ detail.clauseSubTitle }}</h4>
                  <p class="content">{{ detail.clauseSubContent }}</p>

                  <!-- Sub Details -->
                  <div v-if="detail.subDetails?.length > 0" class="sub-details">
                    <div 
                      v-for="subDetail in detail.subDetails" 
                      :key="subDetail.id" 
                      class="sub-detail-item"
                    >
                      <h5>{{ subDetail.subDetailCode }} - {{ subDetail.subDetailTitle }}</h5>
                      <p class="content">{{ subDetail.subDetailContent }}</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Export Options -->
      <div class="export-section">
        <button @click="exportToJSON" class="btn btn-primary">
          Export as JSON
        </button>
        <button @click="exportToCSV" class="btn btn-secondary">
          Export as CSV
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import reportService from '@/services/reportService';

const reports = ref([]);
const loading = ref(false);
const error = ref(null);
const searchTerm = ref('');

// Computed property untuk filter
const filteredReports = computed(() => {
  if (!searchTerm.value) {
    return reports.value;
  }

  return reports.value
    .map(header => ({
      ...header,
      clauses: header.clauses.filter(clause =>
        clause.clauseCode.toLowerCase().includes(searchTerm.value.toLowerCase()) ||
        clause.clauseTitle.toLowerCase().includes(searchTerm.value.toLowerCase())
      )
    }))
    .filter(header => header.clauses.length > 0);
});

// Fetch report data
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
    console.error('Fetch error:', err);
  } finally {
    loading.value = false;
  }
};

// Export to JSON
const exportToJSON = () => {
  const dataStr = JSON.stringify(reports.value, null, 2);
  const dataBlob = new Blob([dataStr], { type: 'application/json' });
  const url = URL.createObjectURL(dataBlob);
  const link = document.createElement('a');
  link.href = url;
  link.download = `report-${new Date().toISOString().split('T')[0]}.json`;
  link.click();
};

// Export to CSV
const exportToCSV = () => {
  const flattened = reportService.flattenForTable(reports.value);
  const headers = [
    'Header Code',
    'Header Title',
    'Clause Code',
    'Clause Title',
    'Detail Code',
    'Detail Title',
    'Sub Detail Code',
    'Sub Detail Title',
    'Sub Detail Content',
    'Is Active'
  ];

  const rows = flattened.map(item => [
    item.headerCode,
    item.headerTitle,
    item.clauseCode,
    item.clauseTitle,
    item.detailCode,
    item.detailTitle,
    item.subDetailCode,
    item.subDetailTitle,
    item.subDetailContent,
    item.isActive ? 'Yes' : 'No'
  ]);

  const csv = [headers, ...rows]
    .map(row => row.map(cell => `"${cell}"`).join(','))
    .join('\n');

  const blob = new Blob([csv], { type: 'text/csv' });
  const url = URL.createObjectURL(blob);
  const link = document.createElement('a');
  link.href = url;
  link.download = `report-${new Date().toISOString().split('T')[0]}.csv`;
  link.click();
};

// Load data on component mount
onMounted(() => {
  fetchReports();
});
</script>

<style scoped>
.report-viewer {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.loading, .error {
  padding: 20px;
  text-align: center;
  border-radius: 4px;
}

.loading {
  background-color: #e3f2fd;
  color: #1976d2;
}

.error {
  background-color: #ffebee;
  color: #c62828;
}

.search-section {
  margin: 20px 0;
}

.search-input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 14px;
}

.report-header {
  margin: 20px 0;
  padding: 15px;
  background-color: #f5f5f5;
  border-left: 4px solid #1976d2;
  border-radius: 4px;
}

.report-header h2 {
  margin: 0 0 10px 0;
  color: #1976d2;
}

.description {
  margin: 10px 0;
  color: #666;
  font-style: italic;
}

.clauses {
  margin: 15px 0;
}

.clause-item {
  margin: 15px 0;
  padding: 12px;
  background-color: #fff;
  border-left: 4px solid #388e3c;
  border-radius: 4px;
}

.clause-item h3 {
  margin: 0 0 10px 0;
  color: #388e3c;
}

.details {
  margin: 15px 0 0 20px;
}

.detail-item {
  margin: 10px 0;
  padding: 10px;
  background-color: #f9f9f9;
  border-left: 4px solid #f57c00;
  border-radius: 4px;
}

.detail-item h4 {
  margin: 0 0 8px 0;
  color: #f57c00;
}

.sub-details {
  margin: 10px 0 0 20px;
}

.sub-detail-item {
  margin: 8px 0;
  padding: 8px;
  background-color: #fff;
  border-left: 4px solid #d32f2f;
  border-radius: 4px;
}

.sub-detail-item h5 {
  margin: 0 0 5px 0;
  color: #d32f2f;
  font-size: 14px;
}

.content {
  margin: 8px 0;
  color: #333;
  line-height: 1.6;
}

.export-section {
  margin: 30px 0;
  padding: 15px;
  background-color: #f5f5f5;
  border-radius: 4px;
  display: flex;
  gap: 10px;
}

.btn {
  padding: 10px 20px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
  transition: background-color 0.3s;
}

.btn-primary {
  background-color: #1976d2;
  color: white;
}

.btn-primary:hover {
  background-color: #1565c0;
}

.btn-secondary {
  background-color: #757575;
  color: white;
}

.btn-secondary:hover {
  background-color: #616161;
}
</style>
```

---

### Using Fetch API

**Alternative approach tanpa Axios:**

```javascript
// src/services/reportService.js (using Fetch)

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000';

const reportService = {
  getAllReports: async () => {
    const response = await fetch(
      `${API_BASE_URL}/api/report/clause-mlci`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        }
      }
    );

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return await response.json();
  },

  getReportById: async (id) => {
    const response = await fetch(
      `${API_BASE_URL}/api/report/clause-mlci/${id}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        }
      }
    );

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return await response.json();
  }
};

export default reportService;
```

---

## Component Examples

### 1. Report Table Component

**File: `src/components/ReportTable.vue`**

```vue
<template>
  <div class="report-table-wrapper">
    <table class="report-table">
      <thead>
        <tr>
          <th>Header Code</th>
          <th>Clause Code</th>
          <th>Clause Title</th>
          <th>Detail Code</th>
          <th>Sub Detail Code</th>
          <th>Status</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="row in tableData" :key="row.subDetailId" class="table-row">
          <td>{{ row.headerCode }}</td>
          <td>{{ row.clauseCode }}</td>
          <td>{{ row.clauseTitle }}</td>
          <td>{{ row.detailCode }}</td>
          <td>{{ row.subDetailCode }}</td>
          <td>
            <span :class="['status', row.isActive ? 'active' : 'inactive']">
              {{ row.isActive ? 'Active' : 'Inactive' }}
            </span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { computed } from 'vue';
import reportService from '@/services/reportService';

const props = defineProps({
  reports: {
    type: Array,
    required: true
  }
});

const tableData = computed(() => {
  return reportService.flattenForTable(props.reports);
});
</script>

<style scoped>
.report-table-wrapper {
  overflow-x: auto;
  border-radius: 4px;
  border: 1px solid #ddd;
}

.report-table {
  width: 100%;
  border-collapse: collapse;
  background-color: white;
}

.report-table thead {
  background-color: #f5f5f5;
  border-bottom: 2px solid #ddd;
}

.report-table th {
  padding: 12px;
  text-align: left;
  font-weight: 600;
  color: #333;
}

.report-table td {
  padding: 10px 12px;
  border-bottom: 1px solid #eee;
}

.table-row:hover {
  background-color: #f9f9f9;
}

.status {
  padding: 4px 8px;
  border-radius: 3px;
  font-size: 12px;
  font-weight: 500;
}

.status.active {
  background-color: #e8f5e9;
  color: #2e7d32;
}

.status.inactive {
  background-color: #ffebee;
  color: #c62828;
}
</style>
```

### 2. Report Accordion Component

**File: `src/components/ReportAccordion.vue`**

```vue
<template>
  <div class="accordion">
    <div 
      v-for="(header, headerIndex) in reports" 
      :key="header.id" 
      class="accordion-item"
    >
      <button 
        class="accordion-header"
        @click="toggleHeader(headerIndex)"
      >
        <span class="toggle-icon">{{ expandedHeaders.includes(headerIndex) ? '?' : '?' }}</span>
        <span>{{ header.clauseHeaderCode }} - {{ header.clauseHeaderTitle }}</span>
      </button>

      <div v-if="expandedHeaders.includes(headerIndex)" class="accordion-content">
        <div 
          v-for="(clause, clauseIndex) in header.clauses" 
          :key="clause.id" 
          class="nested-accordion"
        >
          <button 
            class="nested-header"
            @click="toggleClause(headerIndex, clauseIndex)"
          >
            <span class="toggle-icon">
              {{ expandedClauses[`${headerIndex}-${clauseIndex}`] ? '?' : '?' }}
            </span>
            {{ clause.clauseCode }} - {{ clause.clauseTitle }}
          </button>

          <div v-if="expandedClauses[`${headerIndex}-${clauseIndex}`]" class="nested-content">
            <p class="clause-content">{{ clause.clauseContent }}</p>

            <div 
              v-for="detail in clause.details" 
              :key="detail.id" 
              class="detail-box"
            >
              <h5>{{ detail.clauseSubCode }} - {{ detail.clauseSubTitle }}</h5>
              <p>{{ detail.clauseSubContent }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';

defineProps({
  reports: {
    type: Array,
    required: true
  }
});

const expandedHeaders = ref([]);
const expandedClauses = ref({});

const toggleHeader = (index) => {
  const idx = expandedHeaders.value.indexOf(index);
  if (idx > -1) {
    expandedHeaders.value.splice(idx, 1);
  } else {
    expandedHeaders.value.push(index);
  }
};

const toggleClause = (headerIndex, clauseIndex) => {
  const key = `${headerIndex}-${clauseIndex}`;
  expandedClauses.value[key] = !expandedClauses.value[key];
};
</script>

<style scoped>
.accordion {
  border: 1px solid #ddd;
  border-radius: 4px;
  overflow: hidden;
}

.accordion-item {
  border-bottom: 1px solid #ddd;
}

.accordion-item:last-child {
  border-bottom: none;
}

.accordion-header, .nested-header {
  width: 100%;
  padding: 12px;
  background-color: #f5f5f5;
  border: none;
  text-align: left;
  cursor: pointer;
  font-size: 14px;
  font-weight: 500;
  display: flex;
  align-items: center;
  gap: 8px;
  transition: background-color 0.2s;
}

.accordion-header:hover, .nested-header:hover {
  background-color: #efefef;
}

.nested-header {
  padding-left: 24px;
  background-color: #fafafa;
}

.toggle-icon {
  display: inline-block;
  width: 16px;
}

.accordion-content {
  padding: 12px;
}

.nested-accordion {
  margin: 8px 0;
}

.nested-content {
  padding: 12px;
  background-color: #fafafa;
  border-radius: 4px;
  margin-top: 8px;
}

.clause-content {
  margin: 0 0 12px 0;
  color: #555;
  line-height: 1.6;
}

.detail-box {
  margin: 10px 0;
  padding: 10px;
  background-color: white;
  border-left: 3px solid #1976d2;
  border-radius: 3px;
}

.detail-box h5 {
  margin: 0 0 5px 0;
  color: #1976d2;
}

.detail-box p {
  margin: 0;
  color: #666;
  font-size: 13px;
}
</style>
```

---

## Error Handling

### Global Error Handler

**File: `src/utils/errorHandler.js`**

```javascript
export const handleApiError = (error) => {
  if (error.response) {
    // Server responded with error status
    const { status, data } = error.response;

    switch (status) {
      case 404:
        return 'Report not found';
      case 500:
        return 'Server error. Please try again later.';
      case 503:
        return 'Service unavailable. Please check backend connection.';
      default:
        return data.message || 'An error occurred';
    }
  } else if (error.request) {
    // Request made but no response
    return 'No response from server. Check your connection.';
  } else {
    // Something else happened
    return error.message || 'An unexpected error occurred';
  }
};

export const logError = (component, error, context) => {
  console.error(`[${component}] ${context}:`, error);
  // Kirim ke logging service jika diperlukan
};
```

---

## Troubleshooting

### Issue: CORS Error
**Error:** `Access to XMLHttpRequest has been blocked by CORS policy`

**Solution:**
Pastikan Backend API sudah enable CORS. Di `Program.cs` backend:
```csharp
app.UseCors("AllowVueApp");
```

### Issue: Connection Refused
**Error:** `ERR_CONNECTION_REFUSED`

**Solution:**
1. Pastikan backend API running di port 5000
2. Update `VITE_API_BASE_URL` di `.env.local`
3. Cek firewall settings

### Issue: Empty Data
**Problem:** API returns empty array

**Solution:**
1. Pastikan data ada di database
2. Cek `isActive` flag - hanya data aktif yang di-return
3. Gunakan SQL Server Management Studio untuk verify data

### Issue: Timeout
**Problem:** Request timeout

**Solution:**
1. Tambahkan timeout configuration di service:
```javascript
axios.defaults.timeout = 30000; // 30 seconds
```
2. Cek performance backend query
3. Gunakan pagination untuk large dataset

---

## Testing

### Manual Testing dengan cURL

```bash
# Get all reports
curl -X GET "http://localhost:5000/api/report/clause-mlci" \
  -H "Content-Type: application/json"

# Get specific report
curl -X GET "http://localhost:5000/api/report/clause-mlci/550e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json"
```

### Testing dengan Postman

1. Open Postman
2. Create new GET request
3. URL: `http://localhost:5000/api/report/clause-mlci`
4. Headers: `Content-Type: application/json`
5. Click Send

---

## Performance Tips

1. **Implement Pagination** untuk large dataset
2. **Cache Response** menggunakan localStorage:
```javascript
const getCachedReport = (key) => {
  return JSON.parse(localStorage.getItem(key));
};

const cacheReport = (key, data) => {
  localStorage.setItem(key, JSON.stringify(data));
};
```

3. **Lazy Load** details untuk tree view
4. **Debounce** search input:
```javascript
import { debounce } from 'lodash-es';

const debouncedSearch = debounce(handleSearch, 300);
```

---

## Additional Resources

- [Vue 3 Documentation](https://vuejs.org/)
- [Axios Documentation](https://axios-http.com/)
- [Fetch API Documentation](https://developer.mozilla.org/en-US/docs/Web/API/Fetch_API)
- [Backend API Documentation](./BACKEND_API_DOCUMENTATION.md)

---

## Support

Untuk pertanyaan atau issue, silakan hubungi tim development.

**Last Updated:** 2025-05-24
**Version:** 1.0.0
