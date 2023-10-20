
export type CurrentUser = 
{
    IsAuthenticated : boolean,
    UserName: string,
    Email: string,
    FirstName: string,
    LastName: string,
    authToken: string
};