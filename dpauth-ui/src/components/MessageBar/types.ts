export enum MessageType {    
    Success = 'Success',
    Warning = 'Warning',
    Information = 'Information',
    Error = 'Error'
}

export type MessageBarProp = 
{
    Visible : boolean,
    Text : string,
    Type? : MessageType
}