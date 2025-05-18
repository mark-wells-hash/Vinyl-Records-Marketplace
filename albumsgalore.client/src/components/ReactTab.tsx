//import * as React from 'react';
import { Link} from 'react-router-dom';
import { useState, useEffect } from 'react'
//import { withRouter } from 'react-router-dom';
import { useNavigate } from 'react-router-dom'
import { getUser } from './CommonFunctions';
//import Home from 'Home';
//import Services from './Services';
const ReactTabs = () => {
    const user = getUser();
    const [Home, setHome] = useState("special");
    const [Search, setSearch] = useState("limk");
    const [Community, setCommunity] = useState("limk");
    const [Trivia, setTrivia] = useState("limk");
    const [Artists, setArtists] = useState("link");
    const [Albums, setAlbums] = useState("limk");
    const [Songs, setSongs] = useState("link");
    const [Events, setEvents] = useState("limk");
    const [Offers, setOffers] = useState("link");
    const [Payments, setPayments] = useState("limk");
    const [Uploads, setUploads] = useState("link");
    //const [User, setUser] = useState(null);
    const navigate = useNavigate();   

    useEffect(() => {
        setSongs("link");
        setSearch("link");
        setCommunity("link");
        setTrivia("link");
        setArtists("link");
        setAlbums("link");
        setHome("special");
        setEvents("link");
        setOffers("link");
        setPayments("link");
        setUploads("link");
    }, [])

    const changeStyle = (e: React.MouseEvent<HTMLElement>) => {
        const input = e.target as HTMLElement;
        console.log("you just clicked " + input.id);
        //let currentState = 
        
        //navigate("searchArtist");
        setSongs("link");
        setSearch("link");
        setCommunity("link");
        setTrivia("link");
        setArtists("link");
        setAlbums("link");
        setHome("link");
        setEvents("link");
        setOffers("link");
        setPayments("link");
        setUploads("link");

        switch (input.id) {
            case "Home":
                setHome("special");
                break;
            case "Search":
                setSearch("special");
                break;
            case "Community":
                setCommunity("special");
                break;
            case "Trivia":
                setTrivia("special");
                break;
            case "Artists":
                setArtists("special");
                break;
            case "Albums":
                setAlbums("special");
                break;
            case "Songs":
                setSongs("special");
                break;
            case "Events":
                setEvents("special");
                break;
            case "Offers":
                setOffers("special");
                break;
            case "Payments":
                setPayments("special");
                break;
            case "Uploads":
                setUploads("special");
                break;
            default:
                break;
        }

        if (input.localName === "div") {
            console.log("does it trigger when I click on link?");
            if (input.id === "Home") {
                navigate('/', { replace: true });
            }
            else {
                navigate('/' + input.id, { replace: true });
            }
            
        }
    }

    if (user != null) {
        return (
            <div className="divMenuWrapper">
                <div className={Home} id="Home" onClick={changeStyle} >
                    <Link to='/' className={Home} id="Home" onClick={changeStyle} >
                        <span className='glyphicon glyphicon-th-list'></span> Home
                    </Link>
                </div>
                <div className={Search} id="Search" onClick={changeStyle}>
                    <Link to='/search' className={Search} id="Search" onClick={changeStyle} >
                        <span className='glyphicon glyphicon-th-list'></span> Search
                    </Link>
                </div>
                <div className={Community} id="Community" onClick={changeStyle}>
                    <Link to='/search' className={Community} id="Search" onClick={changeStyle} >
                        <span className='glyphicon glyphicon-th-list'></span> Community
                    </Link>
                </div>
                <div className={Trivia} id="Community" onClick={changeStyle}>
                    <Link to='/search' className={Trivia} id="Search" onClick={changeStyle} >
                        <span className='glyphicon glyphicon-th-list'></span> Music Trivia
                    </Link>
                </div>
                <div className={Artists} id="Artists" onClick={changeStyle}>
                    <Link to='/artists' className={Artists} id="Artists" onClick={changeStyle}>
                        <span className='glyphicon glyphicon-th-list'></span> Artists
                    </Link>
                </div>
                <div className={Albums} id="Albums" onClick={changeStyle}>
                    <Link to='/albums' className={Albums} id="Albums" onClick={changeStyle}>
                        <span className='glyphicon glyphicon-th-list'></span> Albums
                    </Link>
                </div>
                <div className={Songs} id="Songs" onClick={changeStyle}>
                    <Link to='/songs' className={Songs} id="Songs" onClick={changeStyle}>
                        <span className='glyphicon glyphicon-th-list'></span> Songs
                    </Link>
                </div>
                <div className={Events} id="Events" onClick={changeStyle}>
                    <Link to='/events' className={Events} id="Events" onClick={changeStyle} >
                        <span className='glyphicon glyphicon-th-list'></span> Add Events
                    </Link>
                </div>
                <div className={Offers} id="Offers" onClick={changeStyle}>
                    <Link to='/offers' className={Offers} id="Offers" onClick={changeStyle}>
                        <span className='glyphicon glyphicon-th-list'></span> View Offers
                    </Link>
                </div>
                <div className={Payments} id="Payments" onClick={changeStyle} >
                    <Link to='/payments' className={Payments} id="Payments" onClick={changeStyle}>
                        <span className='glyphicon glyphicon-th-list'></span> View Payments
                    </Link>
                </div>
                <div className={Uploads} id="Uploads" onClick={changeStyle}>
                    <Link to='/uploads' className={Uploads} id="Uploads" onClick={changeStyle}>
                        <span className='glyphicon glyphicon-th-list'></span> Upload Data
                    </Link>
                </div>
            </div>
        )
    }
    else {
        return (
            <div className="divMenuWrapper">
                <div className={Home} >
                    <Link to='/' className={Home} id="Home" onClick={changeStyle} >
                        <span className='glyphicon glyphicon-th-list'></span> Home
                    </Link>
                </div>
                <div className={Search} >
                    <Link to='/search' className={Search} id="Search" onClick={changeStyle} >
                        <span className='glyphicon glyphicon-th-list'></span> Search
                    </Link>
                </div>
                <div className={Community} id="Community" onClick={changeStyle}>
                    <Link to='/search' className={Community} id="Search" onClick={changeStyle} >
                        <span className='glyphicon glyphicon-th-list'></span> Community
                    </Link>
                </div>
                <div className={Trivia} id="Community" onClick={changeStyle}>
                    <Link to='/search' className={Trivia} id="Search" onClick={changeStyle} >
                        <span className='glyphicon glyphicon-th-list'></span> Music Trivia
                    </Link>
                </div>
            </div>
        )

    }
}
export default ReactTabs;


