# API Testing Guide - Postman & cURL

This guide provides quick reference for testing the ReportMLCI API endpoints using Postman or cURL.

## Prerequisites

- ReportAPI backend running on `http://localhost:5000`
- Postman installed (optional, but recommended)
- cURL command-line tool (usually pre-installed on most systems)

---

## Testing with cURL

### Master Clause Endpoints

#### 1. Get All Clauses
```bash
curl -X GET "http://localhost:5000/api/masterclause" \
  -H "Content-Type: application/json"
```

#### 2. Get Single Clause
```bash
curl -X GET "http://localhost:5000/api/masterclause/550e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json"
```

#### 3. Create Clause
```bash
curl -X POST "http://localhost:5000/api/masterclause" \
  -H "Content-Type: application/json" \
  -d '{
    "clauseCode": "TEST001",
    "clauseTitle": "Test Clause",
    "clauseContent": "This is a test clause content",
    "isActive": true
  }'
```

#### 4. Update Clause
```bash
curl -X PUT "http://localhost:5000/api/masterclause/550e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json" \
  -d '{
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "clauseCode": "TEST001",
    "clauseTitle": "Updated Test Clause",
    "clauseContent": "Updated test clause content",
    "isActive": true,
    "createdAt": "2024-01-15T10:30:00Z",
    "updatedAt": "2024-01-15T10:30:00Z"
  }'
```

#### 5. Delete Clause
```bash
curl -X DELETE "http://localhost:5000/api/masterclause/550e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json"
```

### Master Clauses Sub Details Endpoints

#### 1. Get All Sub Details
```bash
curl -X GET "http://localhost:5000/api/masterclausessubdetails" \
  -H "Content-Type: application/json"
```

#### 2. Get Single Sub Detail
```bash
curl -X GET "http://localhost:5000/api/masterclausessubdetails/660e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json"
```

#### 3. Create Sub Detail
```bash
curl -X POST "http://localhost:5000/api/masterclausessubdetails" \
  -H "Content-Type: application/json" \
  -d '{
    "masterClauseDetailId": "550e8400-e29b-41d4-a716-446655440100",
    "subDetailCode": "SD001",
    "subDetailTitle": "Test Sub Detail",
    "subDetailContent": "This is a test sub detail content",
    "isActive": true
  }'
```

#### 4. Update Sub Detail
```bash
curl -X PUT "http://localhost:5000/api/masterclausessubdetails/660e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json" \
  -d '{
    "id": "660e8400-e29b-41d4-a716-446655440000",
    "masterClauseDetailId": "550e8400-e29b-41d4-a716-446655440100",
    "subDetailCode": "SD001",
    "subDetailTitle": "Updated Test Sub Detail",
    "subDetailContent": "Updated test sub detail content",
    "isActive": true
  }'
```

#### 5. Delete Sub Detail
```bash
curl -X DELETE "http://localhost:5000/api/masterclausessubdetails/660e8400-e29b-41d4-a716-446655440000" \
  -H "Content-Type: application/json"
```

---

## Postman Collection

Save this as `ReportMLCI-API.postman_collection.json` and import into Postman.

```json
{
  "info": {
    "name": "ReportMLCI API",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
  },
  "item": [
    {
      "name": "Master Clause",
      "item": [
        {
          "name": "Get All",
          "request": {
            "method": "GET",
            "header": [],
            "url": {
              "raw": "{{baseUrl}}/masterclause",
              "host": ["{{baseUrl}}"],
              "path": ["masterclause"]
            }
          }
        },
        {
          "name": "Get by ID",
          "request": {
            "method": "GET",
            "header": [],
            "url": {
              "raw": "{{baseUrl}}/masterclause/{{clauseId}}",
              "host": ["{{baseUrl}}"],
              "path": ["masterclause", "{{clauseId}}"]
            }
          }
        },
        {
          "name": "Create",
          "request": {
            "method": "POST",
            "header": [
              {
                "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
              "mode": "raw",
              "raw": "{\n  \"clauseCode\": \"NEW001\",\n  \"clauseTitle\": \"New Clause\",\n  \"clauseContent\": \"Clause content\",\n  \"isActive\": true\n}"
            },
            "url": {
              "raw": "{{baseUrl}}/masterclause",
              "host": ["{{baseUrl}}"],
              "path": ["masterclause"]
            }
          }
        },
        {
          "name": "Update",
          "request": {
            "method": "PUT",
            "header": [
              {
                "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
              "mode": "raw",
              "raw": "{\n  \"id\": \"{{clauseId}}\",\n  \"clauseCode\": \"NEW001\",\n  \"clauseTitle\": \"Updated Clause\",\n  \"clauseContent\": \"Updated clause content\",\n  \"isActive\": true,\n  \"createdAt\": \"2024-01-15T10:30:00Z\",\n  \"updatedAt\": \"2024-01-15T11:00:00Z\"\n}"
            },
            "url": {
              "raw": "{{baseUrl}}/masterclause/{{clauseId}}",
              "host": ["{{baseUrl}}"],
              "path": ["masterclause", "{{clauseId}}"]
            }
          }
        },
        {
          "name": "Delete",
          "request": {
            "method": "DELETE",
            "header": [],
            "url": {
              "raw": "{{baseUrl}}/masterclause/{{clauseId}}",
              "host": ["{{baseUrl}}"],
              "path": ["masterclause", "{{clauseId}}"]
            }
          }
        }
      ]
    },
    {
      "name": "Master Clauses Sub Details",
      "item": [
        {
          "name": "Get All",
          "request": {
            "method": "GET",
            "header": [],
            "url": {
              "raw": "{{baseUrl}}/masterclausessubdetails",
              "host": ["{{baseUrl}}"],
              "path": ["masterclausessubdetails"]
            }
          }
        },
        {
          "name": "Get by ID",
          "request": {
            "method": "GET",
            "header": [],
            "url": {
              "raw": "{{baseUrl}}/masterclausessubdetails/{{subDetailId}}",
              "host": ["{{baseUrl}}"],
              "path": ["masterclausessubdetails", "{{subDetailId}}"]
            }
          }
        },
        {
          "name": "Create",
          "request": {
            "method": "POST",
            "header": [
              {
                "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
              "mode": "raw",
              "raw": "{\n  \"masterClauseDetailId\": \"{{clauseDetailId}}\",\n  \"subDetailCode\": \"SD001\",\n  \"subDetailTitle\": \"New Sub Detail\",\n  \"subDetailContent\": \"Sub detail content\",\n  \"isActive\": true\n}"
            },
            "url": {
              "raw": "{{baseUrl}}/masterclausessubdetails",
              "host": ["{{baseUrl}}"],
              "path": ["masterclausessubdetails"]
            }
          }
        },
        {
          "name": "Update",
          "request": {
            "method": "PUT",
            "header": [
              {
                "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
              "mode": "raw",
              "raw": "{\n  \"id\": \"{{subDetailId}}\",\n  \"masterClauseDetailId\": \"{{clauseDetailId}}\",\n  \"subDetailCode\": \"SD001\",\n  \"subDetailTitle\": \"Updated Sub Detail\",\n  \"subDetailContent\": \"Updated sub detail content\",\n  \"isActive\": true\n}"
            },
            "url": {
              "raw": "{{baseUrl}}/masterclausessubdetails/{{subDetailId}}",
              "host": ["{{baseUrl}}"],
              "path": ["masterclausessubdetails", "{{subDetailId}}"]
            }
          }
        },
        {
          "name": "Delete",
          "request": {
            "method": "DELETE",
            "header": [],
            "url": {
              "raw": "{{baseUrl}}/masterclausessubdetails/{{subDetailId}}",
              "host": ["{{baseUrl}}"],
              "path": ["masterclausessubdetails", "{{subDetailId}}"]
            }
          }
        }
      ]
    }
  ],
  "variable": [
    {
      "key": "baseUrl",
      "value": "http://localhost:5000/api",
      "type": "string"
    },
    {
      "key": "clauseId",
      "value": "550e8400-e29b-41d4-a716-446655440000",
      "type": "string"
    },
    {
      "key": "clauseDetailId",
      "value": "550e8400-e29b-41d4-a716-446655440100",
      "type": "string"
    },
    {
      "key": "subDetailId",
      "value": "660e8400-e29b-41d4-a716-446655440000",
      "type": "string"
    }
  ]
}
```

### How to Use Postman Collection

1. Open Postman
2. Click "Import" ? Select the JSON file above
3. The collection will be imported with all endpoints
4. Update the variables in the "Variables" tab:
   - `baseUrl`: Your API base URL (e.g., `http://localhost:5000/api`)
   - `clauseId`: A valid UUID for testing
   - `clauseDetailId`: A valid UUID for testing
   - `subDetailId`: A valid UUID for testing
5. Use `{{baseUrl}}`, `{{clauseId}}`, etc. in your requests

---

## Testing Workflow

### Test Create Flow
1. Create a new Master Clause
2. Copy the returned `id` and save it
3. Create Master Clauses Details with the clause `id`
4. Copy the returned `id` and save it
5. Create Master Clauses Sub Details with the clause detail `id`

### Test Read Flow
1. Get all clauses
2. Select a clause by ID
3. Get all sub details
4. Select a sub detail by ID

### Test Update Flow
1. Get a clause by ID
2. Modify the fields
3. Update the clause with PUT
4. Verify the changes with GET

### Test Delete Flow
1. Get all clauses (count them)
2. Delete a clause
3. Verify the clause is gone with GET (should return 404)
4. Get all clauses again (count should decrease)

---

## Environment Setup in Postman

### Step 1: Create Environment
1. Click "Environments" in the left sidebar
2. Click "+" button
3. Name it "ReportMLCI Dev"
4. Add variables:
   ```
   baseUrl: http://localhost:5000/api
   clauseId: (leave empty, will be filled during testing)
   clauseDetailId: (leave empty, will be filled during testing)
   subDetailId: (leave empty, will be filled during testing)
   ```
5. Save the environment
6. Select it from the environment dropdown (top right)

### Step 2: Use Tests to Auto-capture IDs
Add this to your "Create Clause" request under the "Tests" tab:
```javascript
if (pm.response.code === 201) {
    var jsonData = pm.response.json();
    pm.environment.set("clauseId", jsonData.id);
    console.log("Clause ID saved: " + jsonData.id);
}
```

---

## Response Status Codes

| Code | Meaning | Example Scenario |
|------|---------|------------------|
| 200 | OK | GET request successful |
| 201 | Created | POST request successful, resource created |
| 204 | No Content | PUT/DELETE request successful |
| 400 | Bad Request | ID mismatch or invalid data |
| 404 | Not Found | Resource doesn't exist |
| 500 | Server Error | Database error or unhandled exception |

---

## Common Issues & Solutions

### Issue: "Connection refused" error
**Solution:**
- Verify backend is running: `dotnet run` in ReportAPI directory
- Check port is 5000 (or update accordingly)
- Check firewall settings

### Issue: CORS error in browser
**Solution:**
- Add CORS to Program.cs (see BACKEND_API_DOCUMENTATION.md)
- Ensure your Angular app URL is allowed
- Check browser console for detailed error

### Issue: 404 error on valid endpoints
**Solution:**
- Verify endpoint spelling matches exactly
- Check the route attributes in controllers
- Verify parameters are passed correctly

### Issue: 400 error on PUT request
**Solution:**
- Verify ID in URL matches ID in request body
- Ensure all required fields are in request body
- Check data types match the models

### Issue: Database errors on Create/Update
**Solution:**
- Verify foreign key IDs exist before creating related records
- Check field lengths don't exceed database column limits
- Ensure connection string in appsettings.json is correct

---

## Performance Testing

### Load Test with Apache Bench
```bash
# Run 1000 requests with 10 concurrent
ab -n 1000 -c 10 http://localhost:5000/api/masterclause
```

### Monitor API with dotnet-monitor
```bash
# In the ReportAPI directory
dotnet monitor collect -o logs
```

---

## Debugging Tips

### Enable detailed logging
In `appsettings.json`, change log level:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

### Use Postman Console
- Press `Ctrl + Alt + C` to open Postman Console
- View request/response details
- Check for JavaScript errors in tests

### SQL Server Profiler
Monitor actual SQL queries being executed against the database.

---

**Last Updated:** January 2024
**Tools:** Postman, cURL
**API Version:** 1.0
