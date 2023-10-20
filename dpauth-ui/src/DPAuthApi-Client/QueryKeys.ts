import { RegisterUser, UserLogin } from "./data-contracts";
import { Api } from "./Api";
import { RequestParams } from "./http-client";
import { getUser } from "../helpers/login-helper";
import { CurrentUser } from "../../src/providers/AuthContext/Type";

export class QueryKeys{
        
    static loggedInUser = (): CurrentUser => {
        const val = getUser();

        console.log('QueryKeys loggedInUser : ', val ? val : undefined);

        if(val === undefined)
            return {} as CurrentUser;
        else
            return val as CurrentUser;
    }

    static loggedInUserToken = (): string => {
        const val = QueryKeys.loggedInUser();

        console.log('QueryKeys loggedInUser : ', val ? val : undefined);

        if(val === undefined)
            return "" ;
        else
            return val.authToken;
    }

    static api = new Api({
    
        baseUrl: "https://localhost:7275", baseApiParams : { 
            headers : { 
                ContentType: 'application/json',
                Authorization: `Bearer ${QueryKeys.loggedInUserToken()}`
                //Authorization: 'Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI2NTFmMmJjMzRjMjAyZTM3OWNhM2ZkYjIiLCJlbWFpbCI6InN0YXJockBnbWFpbC5jb20iLCJ1bmlxdWVfbmFtZSI6InN0YXJockBnbWFpbC5jb20iLCJuYmYiOjE2OTcxNDUxNTAsImV4cCI6MTY5NzE1MjM1MCwiaWF0IjoxNjk3MTQ1MTUwfQ.t0g8s6eEJhXIrD7CdTLDhtgXhDdpKcCF7r9k5fdtxAsD6N6kzpsCQlQxVZbivx4aqSFlMTGCEHh7hOACBHrG3g'
            }
        }
        });

    static Registration = 
    {
        Key: 'Registration',
        fn: async (data: RegisterUser) =>{
            return this.api.authRegisterCreate(data);
        }
    } 
    static AuthLogin = 
    {
        Key: 'AuthLogin',
        fn: async (data: UserLogin) =>{
            return this.api.authLoginCreate(data);
        }
    } 

    static SendVerificationCode = 
    {
        Key: 'SendVerificationCode',
        fn: async (data?: { emailId?: string | undefined;} | undefined) =>{
            return this.api.authSendVerificationCodeCreate(data);
        }
    } 

    static ChangePassword = 
    {
        Key: 'ChangePassword',
        fn: async (data: UserLogin) =>{
            return this.api.authChangePasswordCreate(data);
        }
    } 

    static GetAllTeams = 
    {
        Key: 'GetAllTeams',
        fn: async () =>{
            return this.api.teamsList();
        }
    } 

    static GetAllUsers = 
    {
        Key: 'GetAllUsers',
        fn: async () =>{
            return this.api.usersUsersList();
        }
    } 

    static GetUserTodos = 
    {
        Key: 'GetUserTodos',
        fn: async (params: RequestParams = {}) =>{
            return this.api.todosTodosList(params);
        }
    } 
}