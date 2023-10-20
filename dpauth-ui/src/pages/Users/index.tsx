import LoadingView from "./LoadingView/index";
import SuccessView from "./SuccessView/index";
import ErrorView from "./ErrorView/index";
import { useQuery } from 'react-query'
import { QueryKeys } from '../../DPAuthApi-Client/QueryKeys';
import Header from "../../components/Header";

const Users = () => {

    console.log(QueryKeys.GetAllUsers.Key);
    const teamsObject = useQuery(QueryKeys.GetAllUsers.Key, QueryKeys.GetAllUsers.fn);
    
    const onAddTodoClick = () => {
      console.log('Add user clicked');
    }
    return (
        <>
            <Header showAddButton={true} headerText="User List" onAddClick={onAddTodoClick} AddButtonText="Add User" />
            {teamsObject.isLoading && <LoadingView />}        
            {teamsObject.isError && <ErrorView />}
            {teamsObject.isSuccess && <SuccessView teams={teamsObject.data?.data} />}
        </>
    );
    
}

export default Users;