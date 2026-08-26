# Quick Start Guide - Vue Report Integration
## Panduan Cepat Mengintegrasikan Report ke Vue

---

## ?? Step-by-Step Setup (5 Menit)

### Step 1: Copy Documentation Files
Dari workspace `ReportMLCI`:
- [ ] Copy `VUE_REPORT_INTEGRATION_GUIDE.md` ke project Vue
- [ ] Copy `VUE_PROJECT_FILES_SETUP.md` ke project Vue

### Step 2: Setup Environment (.env.local)
Create file `.env.local` di root Vue project:

```env
VITE_API_BASE_URL=http://localhost:5000
VITE_REPORT_ENDPOINT=/api/report
VITE_API_TIMEOUT=30000
```

### Step 3: Install Dependencies
```bash
cd your-vue-project
npm install axios
npm install pinia # optional
```

### Step 4: Create Folder Structure
```bash
# Create folders
mkdir -p src/config
mkdir -p src/services
mkdir -p src/composables
mkdir -p src/stores

# Create files (copy dari VUE_PROJECT_FILES_SETUP.md)
# - src/config/api.config.js
# - src/services/httpClient.js
# - src/services/cache.service.js
# - src/services/reportService.js
# - src/composables/useReport.js
# - src/stores/reportStore.js (optional)
```

### Step 5: Create Report Component
Create `src/components/ReportViewer.vue` (copy dari VUE_REPORT_INTEGRATION_GUIDE.md)

### Step 6: Update main.js
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

### Step 7: Gunakan di Component
```vue
<template>
  <ReportViewer />
</template>

<script setup>
import ReportViewer from '@/components/ReportViewer.vue'
</script>
```

---

## ? Verification Checklist

### Backend (ReportAPI)
- [ ] Backend running on `http://localhost:5000`
- [ ] Database connected dengan data
- [ ] CORS enabled di `Program.cs`
- [ ] API endpoint `/api/report/clause-mlci` accessible

**Test Backend:**
```bash
# Using curl
curl http://localhost:5000/api/report/clause-mlci

# Using Postman
GET http://localhost:5000/api/report/clause-mlci
```

### Vue Frontend
- [ ] Node.js installed (v16+)
- [ ] Vue project created
- [ ] Dependencies installed (`axios`, `pinia`)
- [ ] Files created sesuai struktur
- [ ] `.env.local` updated dengan correct URL
- [ ] Import statement correct di components

**Test Frontend:**
```bash
# Run dev server
npm run dev

# Check console untuk errors
# Open http://localhost:5173 (atau port yang ditunjukkan)
```

### Connection Test
- [ ] Open browser console (F12)
- [ ] Cek network requests
- [ ] Verify API response status 200
- [ ] Data displayed di component

---

## ?? Troubleshooting Quick Fixes

### CORS Error
```
? Error: Access to XMLHttpRequest has been blocked by CORS policy
```
**Fix:** Backend `Program.cs` pastikan punya:
```csharp
app.UseCors("AllowVueApp");
```

### Connection Refused
```
? Error: ERR_CONNECTION_REFUSED at http://localhost:5000
```
**Fix:** 
1. Pastikan Backend running
2. Check port di `appsettings.json` atau `launchSettings.json`
3. Update `.env.local` dengan port yang correct

### Empty Data
```
? Array kosong dari API
```
**Fix:**
1. Check database punya data
2. Semua item harus punya `isActive = true`
3. Verify di SQL Server Management Studio

### "Network Error" di Console
```
? Network Error: Error: Network Error
```
**Fix:**
1. Check backend running
2. Check `.env.local` URL correct
3. Check firewall settings
4. Restart beide backend dan frontend

---

## ?? File Structure

```
your-vue-project/
?
??? .env.local (BARU)
?   ??? VITE_API_BASE_URL=http://localhost:5000
?   ??? VITE_REPORT_ENDPOINT=/api/report
?
??? src/
?   ?
?   ??? config/ (BARU)
?   ?   ??? api.config.js
?   ?
?   ??? services/ (UPDATE)
?   ?   ??? httpClient.js (BARU)
?   ?   ??? cache.service.js (BARU)
?   ?   ??? reportService.js (BARU)
?   ?
?   ??? composables/ (BARU)
?   ?   ??? useReport.js
?   ?
?   ??? stores/ (BARU - OPTIONAL)
?   ?   ??? reportStore.js
?   ?
?   ??? components/ (UPDATE)
?   ?   ??? ReportViewer.vue (BARU)
?   ?   ??? ReportTable.vue (BARU)
?   ?   ??? ReportAccordion.vue (BARU)
?   ?
?   ??? App.vue (UPDATE - add component)
?   ??? main.js (UPDATE - add pinia)
?   ??? ...
?
??? ...
```

---

## ?? Usage Examples

### Option 1: Using Composable
```vue
<template>
  <div>
    <div v-if="loading">Loading...</div>
    <div v-else-if="error">{{ error }}</div>
    <div v-else>
      <input v-model="searchTerm" placeholder="Search..." />
      <table>
        <tr v-for="row in tableData" :key="row.subDetailId">
          <td>{{ row.clauseCode }}</td>
          <td>{{ row.clauseTitle }}</td>
        </tr>
      </table>
    </div>
  </div>
</template>

<script setup>
import { onMounted } from 'vue'
import { useReport } from '@/composables/useReport'

const {
  reports,
  loading,
  error,
  searchTerm,
  filteredReports,
  tableData,
  fetchReports
} = useReport()

onMounted(() => {
  fetchReports()
})
</script>
```

### Option 2: Using Store (Pinia)
```vue
<template>
  <div>
    <input 
      v-model="reportStore.searchTerm" 
      placeholder="Search..."
    />
    <button @click="reportStore.fetchReports">Refresh</button>
    <div v-if="reportStore.loading">Loading...</div>
    <div v-else>
      <div v-for="report in reportStore.filteredReports" :key="report.id">
        {{ report.clauseHeaderCode }}
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted } from 'vue'
import { useReportStore } from '@/stores/reportStore'

const reportStore = useReportStore()

onMounted(() => {
  reportStore.fetchReports()
})
</script>
```

### Option 3: Direct Service Call
```vue
<template>
  <div>
    <button @click="load">Load Reports</button>
    <pre>{{ JSON.stringify(reports, null, 2) }}</pre>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { reportService } from '@/services/reportService'

const reports = ref([])

const load = async () => {
  try {
    const response = await reportService.getAllReports()
    reports.value = response.data
  } catch (error) {
    console.error('Error:', error)
  }
}
</script>
```

---

## ?? Testing Endpoints

### Get All Reports
```javascript
// Using fetch
fetch('http://localhost:5000/api/report/clause-mlci')
  .then(r => r.json())
  .then(data => console.log(data))

// Using axios
import axios from 'axios'
axios.get('http://localhost:5000/api/report/clause-mlci')
  .then(r => console.log(r.data))
```

### Get Report by ID
```javascript
const id = '550e8400-e29b-41d4-a716-446655440000'

fetch(`http://localhost:5000/api/report/clause-mlci/${id}`)
  .then(r => r.json())
  .then(data => console.log(data))
```

---

## ?? Sample Response

```json
{
  "success": true,
  "message": "Report data retrieved successfully",
  "data": [
    {
      "id": "550e8400-e29b-41d4-a716-446655440000",
      "clauseHeaderCode": "MLCI-001",
      "clauseHeaderTitle": "Main Clause Header",
      "clauseHeaderDescription": "Description...",
      "isActive": true,
      "clauses": [
        {
          "id": "550e8400-e29b-41d4-a716-446655440001",
          "clauseCode": "C001",
          "clauseTitle": "Clause Title",
          "clauseContent": "Content...",
          "isActive": true,
          "details": [
            {
              "id": "550e8400-e29b-41d4-a716-446655440002",
              "clauseSubCode": "SUB001",
              "clauseSubTitle": "Sub Title",
              "clauseSubContent": "Sub content...",
              "isActive": true,
              "subDetails": [
                {
                  "id": "550e8400-e29b-41d4-a716-446655440003",
                  "subDetailCode": "SUBSUB001",
                  "subDetailTitle": "Sub Detail",
                  "subDetailContent": "Content...",
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

---

## ?? Common Issues & Solutions

| Issue | Symptom | Solution |
|-------|---------|----------|
| CORS Error | Browser blocks request | Enable CORS di backend |
| Connection Refused | Can't reach backend | Check backend running, verify port |
| Empty Array | No data returned | Check database, verify isActive flag |
| 404 Not Found | Report not found | Check ID format, verify data exists |
| Timeout | Request hangs | Increase timeout di config, check backend performance |
| Auth Error | 401 Unauthorized | Add auth token di httpClient interceptor |

---

## ?? Additional Resources

- [VUE_REPORT_INTEGRATION_GUIDE.md](./VUE_REPORT_INTEGRATION_GUIDE.md) - Full detailed guide
- [VUE_PROJECT_FILES_SETUP.md](./VUE_PROJECT_FILES_SETUP.md) - All code files
- [Vue 3 Docs](https://vuejs.org/)
- [Axios Docs](https://axios-http.com/)
- [Pinia Docs](https://pinia.vuejs.org/)

---

## ? Next Steps

1. **Setup** - Ikuti Step 1-7 di atas
2. **Test** - Verify checklist sebelum proceed
3. **Troubleshoot** - Jika ada issue, lihat tabel Common Issues
4. **Customize** - Adapt components sesuai kebutuhan
5. **Deploy** - Deploy ke production

---

## ?? Questions?

Refer ke:
- `VUE_REPORT_INTEGRATION_GUIDE.md` - Detailed documentation
- Backend `BACKEND_API_DOCUMENTATION.md` - API specifications
- Troubleshooting section above

---

**Last Updated:** 2025-05-24  
**Version:** 1.0.0  
**Status:** ? Ready for Use
