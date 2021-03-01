import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { Product } from '../_models';

@Injectable({ providedIn: 'root' })
export class ProductService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Product[]>(`${environment.apiUrl}/products`);
    }

    getProductById(id){
        return this.http.get<Product>(`${environment.apiUrl}/products/${id}`);
    }

    createProduct(formData){
        return this.http.post(`${environment.apiUrl}/products`,formData);
    }

    updateProduct(id,formData){
        return this.http.put(`${environment.apiUrl}/products/${id}`,formData);
    }

    removeProduct(id){
        return this.http.delete(`${environment.apiUrl}/products/${id}`);
    }
}