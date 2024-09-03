export interface AddUser{
    email: string,
    password: string,
    firstName: string,
    lastName?:string,
    countryCode?:string,
    phoneNumber?:string,
    isAdmin: boolean
}