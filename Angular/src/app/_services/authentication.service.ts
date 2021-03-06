﻿import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { User } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string) {
        const headers = new HttpHeaders({ 'Authorization': 'Basic ' + btoa(username + ':' + password)});
     
        return this.http.post<any>('https://dev.sitemercado.com.br/api/login', {username, password}, {headers})
            .pipe(map(user => {

                user.authdata = window.btoa(username + ':' + password);
                if (user.success){
                    localStorage.setItem('currentUser', JSON.stringify(user));
                    this.currentUserSubject.next(user);    
                }
               
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}