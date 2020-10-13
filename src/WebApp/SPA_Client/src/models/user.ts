
import moment from 'moment' 

export interface User {
    FirstName: string;
    LastName: string;
    Email: string;
    Id: string;
    Name: string;
}

export enum AccessLevel {
    Consultant = 'Consultant',
    Supervisor = 'Supervisor',
    Admin = 'Admin'
} 
export interface AuthenticationResult {
    success: boolean;
    message: string;
    user: User;
    timestamp: moment.Moment | null;
}
