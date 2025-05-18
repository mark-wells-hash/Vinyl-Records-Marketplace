import { useEffect } from "react";
import configData from "../config.json";
import { getUser } from './CommonFunctions';
function Logout(){
    console.log("in logout");
    const user = getUser();

    useEffect(() => {
        fetch(configData.SERVER_URL + 'User/AddUserAudit/' + user.userId + '/LogOut')
            .then(response => response.json())
            .then(data => {
                console.log("Here data before " + data )
                
            });
    }, [user])
    localStorage.removeItem('user');
    window.location.href = window.location.origin;
    return (
        <div>
            <h2> Logging out.....</h2>
        </div>
    )
}
export default Logout
