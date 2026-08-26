# Angular TypeScript Models & Services

This document provides ready-to-use TypeScript models and Angular service templates for consuming the ReportMLCI backend API.

## TypeScript Models

### models/master-clause.ts
```typescript
export interface MasterClause {
  id: string;              // UUID
  clauseCode: string;      // Max 50 characters
  clauseTitle: string;     // Max 200 characters
  clauseContent: string;   // Can be very long
  isActive: boolean;
  createdAt: Date;
  updatedAt: Date;
}

export interface MasterClauseCreate extends Omit<MasterClause, 'id' | 'createdAt' | 'updatedAt'> {
  // For creating new records - exclude auto-generated fields
}

export interface MasterClauseUpdate extends MasterClause {
  // For updating records - include all fields
}
```

### models/master-clauses-details.ts
```typescript
export interface MasterClausesDetails {
  id: string;
  masterClauseId: string;  // Foreign Key
  clauseSubCode: string;   // Max 50 characters
  clauseSubTitle: string;  // Max 200 characters
  clauseSubContent: string;// Can be very long
  isActive: boolean;
  createdAt: Date;
  updatedAt: Date;
}

export interface MasterClausesDetailsCreate extends Omit<MasterClausesDetails, 'id' | 'createdAt' | 'updatedAt'> {
  // For creating new records
}

export interface MasterClausesDetailsUpdate extends MasterClausesDetails {
  // For updating records
}
```

### models/master-clauses-sub-details.ts
```typescript
export interface MasterClausesSubDetails {
  id: string;
  masterClauseDetailId: string;  // Foreign Key
  subDetailCode: string;   // Max 10 characters
  subDetailTitle: string;  // Max 200 characters
  subDetailContent: string;// Can be very long
  isActive: boolean;
}

export interface MasterClausesSubDetailsCreate extends Omit<MasterClausesSubDetails, 'id'> {
  // For creating new records
}

export interface MasterClausesSubDetailsUpdate extends MasterClausesSubDetails {
  // For updating records
}
```

### models/master-header-clause.ts
```typescript
export interface MasterHeaderClause {
  id: string;
  clauseHeaderCode: string;      // Max 50 characters
  clauseHeaderTitle: string;     // Max 200 characters
  clauseHeaderDescription: string;// Can be very long
  isActive: boolean;
  createdAt: Date;
  updatedAt: Date;
}

export interface MasterHeaderClauseCreate extends Omit<MasterHeaderClause, 'id' | 'createdAt' | 'updatedAt'> {
  // For creating new records
}

export interface MasterHeaderClauseUpdate extends MasterHeaderClause {
  // For updating records
}
```

### models/index.ts
```typescript
export * from './master-clause';
export * from './master-clauses-details';
export * from './master-clauses-sub-details';
export * from './master-header-clause';
```

---

## Angular Services

### services/master-clause.service.ts
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MasterClause, MasterClauseCreate, MasterClauseUpdate } from '../models';

@Injectable({
  providedIn: 'root'
})
export class MasterClauseService {
  private apiUrl = 'http://localhost:5000/api/masterclause';

  constructor(private http: HttpClient) { }

  /**
   * Retrieve all master clauses
   * @returns Observable of MasterClause array
   */
  getAll(): Observable<MasterClause[]> {
    return this.http.get<MasterClause[]>(this.apiUrl)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Retrieve a single master clause by ID
   * @param id - The UUID of the master clause
   * @returns Observable of MasterClause
   */
  getById(id: string): Observable<MasterClause> {
    return this.http.get<MasterClause>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Create a new master clause
   * @param clause - The master clause data to create
   * @returns Observable of created MasterClause
   */
  create(clause: MasterClauseCreate): Observable<MasterClause> {
    return this.http.post<MasterClause>(this.apiUrl, clause)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Update an existing master clause
   * @param id - The UUID of the master clause to update
   * @param clause - The updated master clause data
   * @returns Observable of void
   */
  update(id: string, clause: MasterClauseUpdate): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, clause)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Delete a master clause
   * @param id - The UUID of the master clause to delete
   * @returns Observable of void
   */
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError(() => new Error('An error occurred: ' + (error.message || 'Unknown error')));
  }
}
```

### services/master-clauses-details.service.ts
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { 
  MasterClausesDetails, 
  MasterClausesDetailsCreate, 
  MasterClausesDetailsUpdate 
} from '../models';

@Injectable({
  providedIn: 'root'
})
export class MasterClausesDetailsService {
  private apiUrl = 'http://localhost:5000/api/masterclausesdetails';

  constructor(private http: HttpClient) { }

  /**
   * Retrieve all master clauses details
   * @returns Observable of MasterClausesDetails array
   */
  getAll(): Observable<MasterClausesDetails[]> {
    return this.http.get<MasterClausesDetails[]>(this.apiUrl)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Retrieve a single master clauses details by ID
   * @param id - The UUID of the details
   * @returns Observable of MasterClausesDetails
   */
  getById(id: string): Observable<MasterClausesDetails> {
    return this.http.get<MasterClausesDetails>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Retrieve all details for a specific master clause
   * @param masterClauseId - The UUID of the master clause
   * @returns Observable of MasterClausesDetails array
   */
  getByMasterClauseId(masterClauseId: string): Observable<MasterClausesDetails[]> {
    // Note: This endpoint may need to be implemented on the backend
    return this.http.get<MasterClausesDetails[]>(`${this.apiUrl}?masterClauseId=${masterClauseId}`)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Create a new master clauses details
   * @param details - The details data to create
   * @returns Observable of created MasterClausesDetails
   */
  create(details: MasterClausesDetailsCreate): Observable<MasterClausesDetails> {
    return this.http.post<MasterClausesDetails>(this.apiUrl, details)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Update an existing master clauses details
   * @param id - The UUID of the details to update
   * @param details - The updated details data
   * @returns Observable of void
   */
  update(id: string, details: MasterClausesDetailsUpdate): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, details)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Delete a master clauses details
   * @param id - The UUID of the details to delete
   * @returns Observable of void
   */
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError(() => new Error('An error occurred: ' + (error.message || 'Unknown error')));
  }
}
```

### services/master-clauses-sub-details.service.ts
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { 
  MasterClausesSubDetails, 
  MasterClausesSubDetailsCreate, 
  MasterClausesSubDetailsUpdate 
} from '../models';

@Injectable({
  providedIn: 'root'
})
export class MasterClausesSubDetailsService {
  private apiUrl = 'http://localhost:5000/api/masterclausessubdetails';

  constructor(private http: HttpClient) { }

  /**
   * Retrieve all master clauses sub details
   * @returns Observable of MasterClausesSubDetails array
   */
  getAll(): Observable<MasterClausesSubDetails[]> {
    return this.http.get<MasterClausesSubDetails[]>(this.apiUrl)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Retrieve a single master clauses sub details by ID
   * @param id - The UUID of the sub details
   * @returns Observable of MasterClausesSubDetails
   */
  getById(id: string): Observable<MasterClausesSubDetails> {
    return this.http.get<MasterClausesSubDetails>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Retrieve all sub details for a specific clause detail
   * @param masterClauseDetailId - The UUID of the clause detail
   * @returns Observable of MasterClausesSubDetails array
   */
  getByMasterClauseDetailId(masterClauseDetailId: string): Observable<MasterClausesSubDetails[]> {
    // Note: This endpoint may need to be implemented on the backend
    return this.http.get<MasterClausesSubDetails[]>(`${this.apiUrl}?masterClauseDetailId=${masterClauseDetailId}`)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Create a new master clauses sub details
   * @param subDetails - The sub details data to create
   * @returns Observable of created MasterClausesSubDetails
   */
  create(subDetails: MasterClausesSubDetailsCreate): Observable<MasterClausesSubDetails> {
    return this.http.post<MasterClausesSubDetails>(this.apiUrl, subDetails)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Update an existing master clauses sub details
   * @param id - The UUID of the sub details to update
   * @param subDetails - The updated sub details data
   * @returns Observable of void
   */
  update(id: string, subDetails: MasterClausesSubDetailsUpdate): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, subDetails)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  /**
   * Delete a master clauses sub details
   * @param id - The UUID of the sub details to delete
   * @returns Observable of void
   */
  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(error => this.handleError(error))
      );
  }

  private handleError(error: any) {
    console.error('API Error:', error);
    return throwError(() => new Error('An error occurred: ' + (error.message || 'Unknown error')));
  }
}
```

### services/index.ts
```typescript
export * from './master-clause.service';
export * from './master-clauses-details.service';
export * from './master-clauses-sub-details.service';
```

---

## HTTP Interceptor (Optional but Recommended)

### interceptors/error.interceptor.ts
```typescript
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'An unknown error occurred';

        if (error.error instanceof ErrorEvent) {
          // Client-side error
          errorMessage = `Error: ${error.error.message}`;
        } else {
          // Server-side error
          errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }

        console.error(errorMessage);
        return throwError(() => new Error(errorMessage));
      })
    );
  }
}
```

### app.module.ts (Configure interceptor)
```typescript
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptor } from './interceptors/error.interceptor';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: []
})
export class AppModule { }
```

---

## Component Examples

### master-clause/master-clause.component.ts
```typescript
import { Component, OnInit, OnDestroy } from '@angular/core';
import { MasterClauseService } from '../../services/master-clause.service';
import { MasterClause, MasterClauseCreate, MasterClauseUpdate } from '../../models';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-master-clause',
  templateUrl: './master-clause.component.html',
  styleUrls: ['./master-clause.component.scss']
})
export class MasterClauseComponent implements OnInit, OnDestroy {
  clauses: MasterClause[] = [];
  loading = false;
  error: string | null = null;
  selectedClause: MasterClause | null = null;
  showForm = false;
  isEditing = false;
  private destroy$ = new Subject<void>();

  constructor(private clauseService: MasterClauseService) { }

  ngOnInit(): void {
    this.loadClauses();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadClauses(): void {
    this.loading = true;
    this.error = null;
    this.clauseService.getAll()
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (data) => {
          this.clauses = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load clauses: ' + err.message;
          this.loading = false;
        }
      });
  }

  onSelectClause(clause: MasterClause): void {
    this.selectedClause = clause;
    this.isEditing = true;
    this.showForm = true;
  }

  onNewClause(): void {
    this.selectedClause = null;
    this.isEditing = false;
    this.showForm = true;
  }

  onSave(clause: MasterClause): void {
    if (this.isEditing && this.selectedClause) {
      const updateData: MasterClauseUpdate = {
        ...clause,
        id: this.selectedClause.id,
        createdAt: this.selectedClause.createdAt,
        updatedAt: new Date()
      };
      this.clauseService.update(this.selectedClause.id, updateData)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: () => {
            alert('Clause updated successfully');
            this.loadClauses();
            this.showForm = false;
          },
          error: (err) => {
            this.error = 'Failed to update clause: ' + err.message;
          }
        });
    } else {
      const createData: MasterClauseCreate = {
        clauseCode: clause.clauseCode,
        clauseTitle: clause.clauseTitle,
        clauseContent: clause.clauseContent,
        isActive: clause.isActive
      };
      this.clauseService.create(createData)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: () => {
            alert('Clause created successfully');
            this.loadClauses();
            this.showForm = false;
          },
          error: (err) => {
            this.error = 'Failed to create clause: ' + err.message;
          }
        });
    }
  }

  onDelete(id: string): void {
    if (confirm('Are you sure you want to delete this clause?')) {
      this.clauseService.delete(id)
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: () => {
            alert('Clause deleted successfully');
            this.loadClauses();
          },
          error: (err) => {
            this.error = 'Failed to delete clause: ' + err.message;
          }
        });
    }
  }

  onCancel(): void {
    this.showForm = false;
    this.selectedClause = null;
  }
}
```

### master-clause/master-clause.component.html
```html
<div class="container">
  <h1>Master Clauses</h1>

  <div *ngIf="error" class="alert alert-danger">
    {{ error }}
  </div>

  <div *ngIf="loading" class="alert alert-info">
    Loading...
  </div>

  <button *ngIf="!showForm" class="btn btn-primary mb-3" (click)="onNewClause()">
    Add New Clause
  </button>

  <div *ngIf="!showForm" class="clause-list">
    <div *ngFor="let clause of clauses" class="clause-item card mb-2">
      <div class="card-body">
        <h5 class="card-title">{{ clause.clauseTitle }}</h5>
        <p class="card-text">Code: {{ clause.clauseCode }}</p>
        <p class="card-text">Status: {{ clause.isActive ? 'Active' : 'Inactive' }}</p>
        <button class="btn btn-sm btn-warning" (click)="onSelectClause(clause)">
          Edit
        </button>
        <button class="btn btn-sm btn-danger" (click)="onDelete(clause.id)">
          Delete
        </button>
      </div>
    </div>
  </div>

  <div *ngIf="showForm" class="form-section">
    <!-- Add your form component here -->
  </div>
</div>
```

---

## Environment Configuration

### environment.ts
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

### environment.prod.ts
```typescript
export const environment = {
  production: true,
  apiUrl: 'https://api.yourdomain.com/api'
};
```

---

## Best Practices

1. **Always unsubscribe** - Use `takeUntil()` pattern to prevent memory leaks
2. **Error handling** - Implement proper error handling in all service calls
3. **Loading states** - Show loading indicators to users
4. **Type safety** - Use TypeScript interfaces for all API models
5. **Environment variables** - Use different API URLs for dev/prod
6. **CORS handling** - Ensure backend CORS policy allows your Angular app

---

**Created:** January 2024
**For:** Angular 17+
**API Base:** ASP.NET Core 8
