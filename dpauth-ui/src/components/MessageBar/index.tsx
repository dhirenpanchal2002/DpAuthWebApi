import React, { useEffect, useState } from 'react'
import { MessageBarProp, MessageType } from './types'
import { AlertColor, Snackbar } from '@mui/material'
import MuiAlert, { AlertProps } from '@mui/material/Alert';

const CustomAlert = React.forwardRef<HTMLDivElement, AlertProps>(function Alert(
  props,
  ref,
) {
  return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

interface Props
{
    messageBarProp : MessageBarProp
}

export default function MessageBar({ messageBarProp } : Props) {
    const [isOpen,SetIsOpen] = useState(messageBarProp.Visible);
  
    useEffect(() => {
        
        SetIsOpen(messageBarProp.Visible);
      
    }, [messageBarProp])
    
    const getAlertColor = () : AlertColor => 
    {
        switch(messageBarProp.Type)
        {
            case MessageType.Error: 
                return "error" as AlertColor;
            case MessageType.Success: 
                return "success" as AlertColor;
            case MessageType.Warning: 
                return "warning" as AlertColor;
            default:    
                return "info" as AlertColor;
        }
    };  
    
    const handleClose = () => {
        SetIsOpen(false);
      };

  return (
    <Snackbar open={isOpen} autoHideDuration={6000} onClose={handleClose}
        anchorOrigin={{horizontal: 'center', vertical : 'bottom' }}>
        <CustomAlert onClose={handleClose}             
            severity={getAlertColor()} 
            sx={{ width: '100%' }}>
            {messageBarProp.Text}
        </CustomAlert>
    </Snackbar>
  )
}
