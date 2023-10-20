import React, { useState } from 'react'
import { TodoDetail, TodoStatus } from './../../DPAuthApi-Client/data-contracts';
import TodoItem from '../../components/TodoItem';
import { Box } from '@mui/material';
import TodoView from '../TodoView';
import { viewMode } from '../TodoView/type';
import Header from '../../components/Header';
import { useQuery } from 'react-query'
import { QueryKeys } from '../../DPAuthApi-Client/QueryKeys';

const Todos =() => {    
    const [showTodoItem,SetShowTodoItem] = useState<TodoDetail>({});
    const [open, setOpen] = React.useState<boolean>(false);
    const [openMode, setOpenMode] = React.useState<viewMode>(viewMode.Edit);

    console.log(QueryKeys.GetUserTodos.Key);
    const todoObject = useQuery(QueryKeys.GetUserTodos.Key, () => QueryKeys.GetUserTodos.fn());
    
    const onShowTodoClick = (todo: TodoDetail) => {
        SetShowTodoItem(todo);
        setOpenMode(viewMode.Display);
        setOpen(true);
    }

    const onEditTodoClick = (todo: TodoDetail) => {
        SetShowTodoItem(todo);
        setOpenMode(viewMode.Edit);
        setOpen(true);
    }

    const onCompleteTodoClick = (todo: TodoDetail) => {
        SetShowTodoItem(todo);
        setOpenMode(viewMode.Edit);
        setOpen(true);
    }

    const onDeleteTodoClick = (todo: TodoDetail) => {
        SetShowTodoItem(todo);
        setOpenMode(viewMode.Edit);
        setOpen(true);
    }

    const onCloseTodoClick = () => {
        setOpen(false);
    }

    const onAddTodoClick = () => {
        SetShowTodoItem({ id : 'new', summary : '', description : '', status: TodoStatus.Pending});        
        setOpenMode(viewMode.Add);
        setOpen(true);
    }

    return (
        <>  
            <Header showAddButton={true} headerText="Todos" onAddClick={onAddTodoClick} AddButtonText="Add Todo" />
            <Box sx={{ pl: '0vh' }}>
                {todoObject.data?.data.map((todo) => (
                    <TodoItem key={todo.id} TodoDetail={todo} 
                        showTodoHandler={onShowTodoClick.bind(todo)}
                        EditTodoHandler={onEditTodoClick.bind(todo)}
                        CompleteTodoHandler={onCompleteTodoClick.bind(todo)}
                        DeleteTodoHandler={onDeleteTodoClick.bind(todo)}
                        />
                    )
                )}
            </Box>
            <TodoView openMode={openMode} todoData={showTodoItem} onClose={onCloseTodoClick} showDialog={open} ></TodoView>
        </>
       )
}

export default Todos;