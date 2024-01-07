import { Color } from "@mui/material";
import theme from "../theme";
import { ColorLens } from "@mui/icons-material";
import { blue, green, orange, red } from "@mui/material/colors";

const isNullorEmpty = (val: object | string | null | undefined) : boolean =>
{   
    if(val === null || val === 'undefined')
        return true;

    if(val === '')    
        return true;
        
    return false;
}

export default isNullorEmpty;

export const getTodoStatusColor = (todo : string | null | undefined) : Color => {

    if(todo === undefined || todo == null)
        return blue;  //(theme) => theme.palette.primary[600]

    switch(todo)
    {
        case "pending": return orange;
        case "completed": return green;
        case "inprogress": return blue;
        case "cancelled": return red;
        default: return  blue;
    }
}