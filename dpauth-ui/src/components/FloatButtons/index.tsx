import * as React from 'react';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import DoneIcon from '@mui/icons-material/Done';
import { Box, IconButton} from '@mui/material';

interface Props
{
    CompleteHandler?: () => void,
    EditHandler?: () => void,
    DeleteHandler?: () => void
}

export default function FloatButtons( {CompleteHandler,EditHandler,DeleteHandler } : Props) {
  
  
  return (
    <Box sx={{ display:'inline-flex',flexDirection:['column','row'],justifyContent:'space-between', alignSelf:'flex-start'}}>
        <IconButton onClick={CompleteHandler}>
            <DoneIcon color='success' fontSize='small' sx={{mb:'1px', mt:'0px'}} />
        </IconButton>
        <IconButton onClick={EditHandler}>
        <EditIcon color='secondary' fontSize='small'  sx={{mb:'1px'}}/>
        </IconButton>
        <IconButton onClick={DeleteHandler}>
        <DeleteIcon color='error' fontSize='small'  sx={{m:'0px'}}/>
        </IconButton>
    </Box>
  );
}
