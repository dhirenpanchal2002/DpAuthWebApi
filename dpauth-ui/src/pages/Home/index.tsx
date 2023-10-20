import Header from "../../components/Header";
import * as React from 'react';
import Box from '@mui/system/Box';
import Grid from '@mui/system/Unstable_Grid';
import styled from '@mui/system/styled';

const Item = styled('div')(({ theme }) => ({
  backgroundColor: theme.palette.mode === 'dark' ? '#1A2027' : '#fff',
  border: '1px solid',
  borderColor: theme.palette.mode === 'dark' ? '#444d58' : '#ced7e0',
  padding: theme.spacing(1),
  borderRadius: '4px',
  height: '300px',
  width: '300px',
  textAlign: 'center',
}));

const Home = () => {
       
    const onTileClick = (event: React.MouseEvent<HTMLElement>) => {
        console.log(`Tile clicked ${event.currentTarget.innerText}`);
     }
    return (
        <>
            <Header showAddButton={true} headerText="Home" AddButtonText={""}  />    
            <Box sx={{ flexGrow: 1, p:'2vh' }}>
                <Grid container spacing={{ xs: 2, md: 2 }} columns={{ xs: 4, sm: 4, md: 8, lg: 8, xl: 16 }}>
                    {Array.from(Array(4)).map((_, index) => (
                    <Grid xs={2} sm={4} key={index}>
                        <Item onClick={onTileClick}>{index + 1}</Item>
                    </Grid>
                    ))}
                </Grid>
            </Box>
        </>
    );
    
}
export default Home;