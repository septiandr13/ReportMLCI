# ?? QUICK REFERENCE CARD
## Print This & Keep as Reference

---

## ?? TL;DR (Too Long; Didn't Read)

### Setup dalam 3 Langkah:

```
1. Copy files dari VUE_PROJECT_FILES_SETUP.md
2. Follow VUE_QUICK_START.md Step 1-7
3. Open browser ? Done!
```

---

## ?? Checklist

### Before Setup ?
- [ ] Backend running: `http://localhost:5000`
- [ ] Database connected
- [ ] Vue project ready
- [ ] Node.js installed

### During Setup ?
- [ ] Create .env.local
- [ ] Copy 7 service files
- [ ] Copy 3 components
- [ ] Update main.js

### After Setup ?
- [ ] `npm run dev` works
- [ ] No console errors
- [ ] API responds 200
- [ ] Data displays

---

## ?? API Endpoints

```bash
# Get All
GET http://localhost:5000/api/report/clause-mlci

# Get By ID
GET http://localhost:5000/api/report/clause-mlci/{id}
```

---

## ?? Folder Structure

```
src/
??? config/
?   ??? api.config.js
??? services/
?   ??? httpClient.js
?   ??? cache.service.js
?   ??? reportService.js
??? composables/
?   ??? useReport.js
??? stores/
?   ??? reportStore.js
??? components/
    ??? ReportViewer.vue
    ??? ReportTable.vue
    ??? ReportAccordion.vue
```

---

## ?? Quick Commands

```bash
# Setup
npm install axios pinia
mkdir -p src/{config,services,composables,stores}

# Run
npm run dev

# Test API
curl http://localhost:5000/api/report/clause-mlci

# Check cache
console.log(reportService.getCacheStats())
```

---

## ?? Common Issues

| Problem | Solution |
|---------|----------|
| CORS Error | Enable CORS in backend Program.cs |
| 404 API | Check backend running on :5000 |
| Empty data | Verify database has data, isActive=true |
| Timeout | Increase timeout in api.config.js |
| Auth error | Add token to httpClient interceptor |

---

## ?? 3 Ways to Use

### Option 1: Composable
```javascript
const { reports, loading, fetchReports } = useReport()
onMounted(() => fetchReports())
```

### Option 2: Store (Pinia)
```javascript
const store = useReportStore()
store.fetchReports()
watch(() => store.selectedReport, (report) => {...})
```

### Option 3: Direct Service
```javascript
const response = await reportService.getAllReports()
```

---

## ?? Response Example

```json
{
  "success": true,
  "data": [
    {
      "id": "...",
      "clauseHeaderCode": "MLCI-001",
      "clauses": [
        {
          "clauseCode": "C001",
          "details": [
            {
              "clauseSubCode": "SUB001",
              "subDetails": [...]
            }
          ]
        }
      ]
    }
  ]
}
```

---

## ?? Configuration

### .env.local
```
VITE_API_BASE_URL=http://localhost:5000
VITE_REPORT_ENDPOINT=/api/report
VITE_API_TIMEOUT=30000
```

### api.config.js
```javascript
baseURL: import.meta.env.VITE_API_BASE_URL
timeout: 30000
retry: 3 attempts dengan 1s delay
cache: 5 minutes
```

---

## ?? File Reading Order

1. ? INDEX_VUE_INTEGRATION.md
2. ? VUE_QUICK_START.md
3. ?? VUE_PROJECT_FILES_SETUP.md
4. ?? VUE_REPORT_INTEGRATION_GUIDE.md
5. ?? README_VUE_INTEGRATION.md

---

## ? Features

? API Integration dengan retry  
? Hierarchical data display  
? Search & filter  
? Export JSON/CSV  
? Memory caching (5 min)  
? Error handling  
? Loading states  
? Composable & Store options  

---

## ?? Data Flow

```
Component
    ?
useReport / useReportStore
    ?
reportService
    ?
httpClient (with cache)
    ?
API: localhost:5000/api/report/clause-mlci
    ?
Backend ? Database
```

---

## ?? Next Steps

1. **Copy** all files
2. **Follow** VUE_QUICK_START.md
3. **Test** API endpoints
4. **Verify** in browser
5. **Customize** as needed

---

## ?? Support

- Setup issues ? VUE_QUICK_START.md
- Error handling ? VUE_REPORT_INTEGRATION_GUIDE.md
- Code ? VUE_PROJECT_FILES_SETUP.md
- Navigation ? INDEX_VUE_INTEGRATION.md

---

## ?? Status

? Backend: Ready  
? API: Ready  
? Documentation: Complete  
? Code: Production Ready  
? Components: 3 examples  
? Services: Full featured  

---

## ?? Time Estimates

- Setup: 5-15 minutes
- Testing: 5 minutes
- Customization: 30-60 minutes
- Deployment: 10-20 minutes

**Total:** 1-2 hours to production

---

## ?? Browser Support

? Chrome/Edge (Latest)  
? Firefox (Latest)  
? Safari (Latest)  
? Mobile browsers  

---

## ?? Security

- CORS enabled (configure for production)
- Token support (uncomment in httpClient)
- Error handling (no sensitive data leaks)
- Input validation (ready to add)

---

## ?? Learning Resources

- Vue 3: vuejs.org
- Axios: axios-http.com
- Pinia: pinia.vuejs.org
- This docs: Complete package

---

## ? Go Live Checklist

- [ ] Backend production URL set
- [ ] Database production connection
- [ ] CORS configured properly
- [ ] Error logging enabled
- [ ] Performance tested
- [ ] Security reviewed
- [ ] Backup created

---

## ?? Ready to Start?

? Open: **INDEX_VUE_INTEGRATION.md**

---

**Print & Keep This Handy!**
