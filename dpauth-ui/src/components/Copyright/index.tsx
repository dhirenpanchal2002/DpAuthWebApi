import { Container, Link, Typography } from "@mui/material";

const Copyright = (props: any) => {
    return (
      <Container>
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
          {'Copyright © '}
          <Link color="inherit" href="https://mui.com/">
            www.star-hr.com
          </Link>{' '}
          {new Date().getFullYear()}
          {'.'}
        </Typography>
      </Container> 
    );
  }

  export default Copyright;