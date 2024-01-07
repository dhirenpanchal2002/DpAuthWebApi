import React, { useEffect, useState } from 'react'
import { TodoDetail, TodoStatus } from './../../DPAuthApi-Client/data-contracts';
import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, FormControl, IconButton, InputLabel, MenuItem, Select, SelectChangeEvent, TextField, Typography } from '@mui/material';
import { viewMode } from './type';

interface Props
{
    todoData: TodoDetail,
    showDialog: boolean,
    openMode: viewMode,
    onClose: () => void,
    onSave?: () => void
}
const TodoView =({todoData,showDialog,openMode, onClose, onSave } : Props) => {
    
    const [todo, SetTodo] = useState<TodoDetail>(todoData);
    console.log('Set openMode', openMode !== viewMode.Add);
    console.log('Set do', todo);

    useEffect(() => {
        SetTodo(todoData);
    }, [todoData])
    
    const handleOnChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if(event.target.id === "summary")
        {
            SetTodo({...todo, summary : event.target.value});
        }
        if(event.target.id === "description")
        {
            SetTodo({...todo, description : event.target.value});
        }
    }

    const handleClose = () => {
        if(onClose)
            onClose();
      };
    
    const handleSave = () => {
        if(onSave)
            onSave();
      };
      
 
    const handleStatusChange = (event: SelectChangeEvent) => {
        const newStatus = event.target.value as TodoStatus;
        SetTodo({...todo, status : newStatus});
    };
    return (        

        <div>            
            <Dialog open={showDialog} onClose={handleClose} sx={{minWidth : '800px'}}>
                <DialogTitle sx={{ borderBottom: 'solid blue', m: '2vh', p:'2vh'}}> 
                        {openMode === viewMode.Add && 'Add new todo'}
                        {openMode === viewMode.Edit && 'Edit todo'}
                        {openMode === viewMode.Display && 'View todo'}
                  
                </DialogTitle>
                <DialogContent sx={{backgroundColor: (theme) => theme.palette.grey[100], mt: '0vh', ml: '0vh', mr: '0vh', borderRadius:'4px'}}>
                    
                        <FormControl variant="outlined" fullWidth>
                            <TextField disabled={openMode=== viewMode.Display} margin="dense" id="summary"  onChange={handleOnChange}
                                label="Summary" type="text"  variant="outlined" value={todo.summary} />
                        </FormControl>
                        <FormControl variant="outlined" fullWidth>
                            <TextField multiline disabled={openMode=== viewMode.Display} margin="dense" id="description" onChange={handleOnChange}
                                label="Description" type="text" variant="outlined" value={todo.description} />                    
                        </FormControl>
                        <FormControl variant="outlined" sx={{ mt: '2vh', minWidth: 120 }}>
                            <InputLabel id="todostatus-label">Status</InputLabel>
                            <Select disabled={openMode== viewMode.Display} 
                            labelId="todostatus-label"
                            id="todostatus-select"
                            value={todo.status ? todo.status : TodoStatus.Pending}
                            label="Status"
                            onChange={handleStatusChange}
                            >
                            <MenuItem value={TodoStatus.Completed}>{TodoStatus.Completed}</MenuItem>
                            <MenuItem value={TodoStatus.InProgress}>{TodoStatus.InProgress}</MenuItem>
                            <MenuItem value={TodoStatus.Pending}>{TodoStatus.Pending}</MenuItem>
                            </Select>
                        </FormControl>
                    
                </DialogContent>
                <DialogActions sx={{ p: '2vh' }}>
                    {openMode !== viewMode.Display && <Button variant='contained' onClick={handleSave}>Save</Button>}
                    <Button variant='contained' onClick={handleClose}>Cancel</Button>
                </DialogActions>
            </Dialog>
        </div>
       )
}

export default TodoView;