export interface User {
    id: string;
    Username: string;
    Email?: string;
    PasswordHash?: string;
    CreatedDate?: Date;
    UpdatedDate?: Date;
}