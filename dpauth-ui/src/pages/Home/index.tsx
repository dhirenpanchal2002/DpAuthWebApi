import * as React from 'react';
import Box from '@mui/system/Box';
import Grid from '@mui/system/Unstable_Grid';
import styled from '@mui/system/styled';
import {Typography } from "@mui/material";

const Item = styled('div')(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  border: '1px solid',
  borderColor: theme.palette.mode === 'dark' ? '#444d58' : '#ced7e0',
  padding: theme.spacing(1),
  borderRadius: '8px',
  height: '200px',
  width: '200px',
  verticalAlign: 'middle',
  textAlign: 'center',
  cursor:'hand'
}));

const Home = () => {
       
    const onTileClick = (event: React.MouseEvent<HTMLElement>) => {
        console.log(`Tile clicked ${event.currentTarget.innerText}`);
     }
    return (
        <>
            <Box sx={{ flexGrow: 1, p:'6vh' }}>
                <Grid container spacing={3} columns={{ xs: 4, sm: 8, md: 12 }}
                    justifyContent="space-evenly"
                    alignItems="center">
                    <Grid xs={3} key={1}>
                        <Item onClick={onTileClick}>
                            <Typography variant="h4">
                                Dashboard
                            </Typography>    
                            <Typography variant="body2">
                                Overall view of your tasks, groups and users
                            </Typography>    
                        </Item>
                    </Grid>
                    <Grid xs={3} key={2}>
                        <Item onClick={onTileClick} >
                            <Typography variant="h4">
                                Tasks
                            </Typography>    
                            <Typography variant="body2">
                                Manage and track your tasks along with their status
                            </Typography>   
                        </Item>
                    </Grid>
                    <Grid xs={3} key={3}>
                        <Item onClick={onTileClick}>        
                            <Typography variant="h4">
                                Users
                            </Typography>
                            <Typography variant="body2">
                                Manage and users and their details
                            </Typography>   
                        </Item>
                    </Grid>
                    <Grid xs={3} key={4}>
                        <Item onClick={onTileClick}>     
                            <Typography variant="h4">
                                Groups
                            </Typography>
                            <Typography variant="body2">
                                Manage groups and group members
                            </Typography>   
                        </Item>
                    </Grid>
                </Grid>
            </Box>
        </>
    );
    
}
export default Home;