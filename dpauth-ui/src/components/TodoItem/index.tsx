import { Box, Card, Chip, Link, Typography } from "@mui/material";
import { TodoDetail } from './../../DPAuthApi-Client/data-contracts';

import React from "react";
import FloatButtons from "../FloatButtons";
import { getTodoStatusColor } from "../../helpers/helpers";

interface Props
{
    TodoDetail : TodoDetail,
    showTodoHandler : (todo: TodoDetail) => void
    CompleteTodoHandler : (todo: TodoDetail) => void
    EditTodoHandler : (todo: TodoDetail) => void
    DeleteTodoHandler : (todo: TodoDetail) => void
}

const TodoItem = ({TodoDetail, showTodoHandler, CompleteTodoHandler, EditTodoHandler,DeleteTodoHandler } : Props) => {
    
    const showTodoClick = (event: React.MouseEvent<HTMLElement>) => {
        event?.preventDefault();             
        if(showTodoHandler)
        {
            console.log("Show todo click handler call " + TodoDetail.id);
            showTodoHandler(TodoDetail);            
        }
    };

    const EditClick = () => {
        if(EditTodoHandler)
        {
            console.log("Edit click handler call " + TodoDetail.id);
            EditTodoHandler(TodoDetail);            
        }
    };

    const CompleteClick = () => {
        if(CompleteTodoHandler)
        {
            console.log("Complete click handler call " + TodoDetail.id);
            CompleteTodoHandler(TodoDetail);            
        }
    };

    const DeleteClick = () => {
        if(DeleteTodoHandler)
        {
            console.log("Delete click handler call " + TodoDetail.id);
            DeleteTodoHandler(TodoDetail);            
        }
    };

    return (
    <Card sx={{ borderRadius:[1,1,2,2,2], padding: '2vh', margin: '1vh', 
        display:'flex', flexDirection:['row'], justifyContent:'space-between'}} >
        <Box sx={{ display: 'inline-block', flexGrow:'inherit' }}>
            <Typography variant="body1"><Link onClick={showTodoClick}>{TodoDetail.summary}</Link></Typography>
            <Typography variant="subtitle1">{TodoDetail.description}</Typography>
            <Chip variant="outlined"
            style={{ backgroundColor: getTodoStatusColor(TodoDetail.status)[300]}}
            size="small" label={TodoDetail.status} />
        </Box>
        <FloatButtons EditHandler={EditClick} CompleteHandler={CompleteClick} DeleteHandler={DeleteClick} />
    </Card>)
}

export default TodoItem;
