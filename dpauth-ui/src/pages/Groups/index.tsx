import LoadingView from "./LoadingView/index";
import SuccessView from "./SuccessView/index";
import ErrorView from "./ErrorView/index";
import { useQuery } from 'react-query'
import { QueryKeys } from '../../DPAuthApi-Client/QueryKeys';
import Header from "../../components/Header";

const Groups = () => {

    console.log(QueryKeys.GetAllTeams.Key);
    const teamsObject = useQuery(QueryKeys.GetAllTeams.Key, QueryKeys.GetAllTeams.fn);
    
    const onAddTodoClick = () => {
      console.log('Add clicked');
    }
    return (
        <>
            <Header showAddButton={true} headerText="Group List" onAddClick={onAddTodoClick} AddButtonText="Add Group" />
            {teamsObject.isLoading && <LoadingView />}        
            {teamsObject.isError && <ErrorView />}
            {teamsObject.isSuccess && <SuccessView teams={teamsObject.data?.data} />}
        </>
    );
    
}

export default Groups;