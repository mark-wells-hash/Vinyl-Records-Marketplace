import { useState} from 'react'
import { Link } from 'react-router-dom';
import { getUser } from './CommonFunctions';
import configData from "../config.json";
//import { UserContext } from './CartCountContext';

//export default class Header extends React.Component<IUser> {
const Header = () => {
    const user = getUser();
    //let salesCount = 0;
    const [salesCount, setSalesCount] = useState(0);
    //TODO: Maybe come back to this. For now work around with a hover event
    //const userContext = useContext(UserContext);
    //useEffect(() => {
    //userContext?.updateCount(8);

    //fetch(configData.SERVER_URL + 'Sales/GetShoppingCartCountByUser/' + user.userId)
    //    .then((data) => data.json())
    //    .then((data) => {
    //        console.log("In Header fetch: " + data);
    //        setSalesCount(data);
    //    });

    //}, [])

    console.log("In Header after fetch: " + salesCount );

    if (user != null) {
        return (
            <div className='Header'>
                <img src="../../record with gold trim.jpg" className="logoImage" />
                <div className="topDiv">
                    <h1>Vinyl Record Marketplace</h1>
                </div>
                <div className="topMenu">
                    <div className="shoppingCartContainer">
                        <Link to='/shoppingCart' className="" id="shoppingCart">
                            <img src="../ShoppingCart.png" alt="Shopping Cart" title="Shopping Cart" className="" />
                            <div className="shoppingCartCentered shoppingCartText">{salesCount}</div>
                        </Link>
                    </div>
                    <Link to='/logout' className="topMenuItem" id="Logout">
                        <img src="../icons8-logout-100.png" alt="Logout" title="Logout" className="iconSizeBiggerPlus" />
                    </Link>
                    <Link to='/profile' className="topMenuItem" id="Profile">
                        <img src="../Profile2.png" alt="Profile" title="Profile" className="iconSizeBigger" />
                    </Link>
                </div>
            </div>
        )
    }
    else {
        return (
            <div className='Header'>
                <img src="../../record with gold trim.jpg" />
                <div className="topDiv">
                    <h1>Vinyl Record Marketplace</h1>
                </div>
                <div className="topMenu2">
                    <Link to='/login' className="topMenuItemAlbumDetails" id="Login">
                        <img src="../login_5800052.png" alt="Login" title="Login" className="iconSizeBigger" />
                    </Link>
                    <Link to='/register' className="topMenuItemAlbumDetails" id="Signup">
                        <img src="../upload_14292699.png" alt="Register" title="Register" className="iconSizeBigger" />
                    </Link>
                </div>
            </div>
        )
    }

}

export default Header