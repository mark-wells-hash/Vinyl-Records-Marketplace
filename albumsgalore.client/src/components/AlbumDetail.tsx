import { useState, useEffect, MouseEvent, useContext } from 'react'
import { useParams } from 'react-router';
import { useNavigate } from "react-router-dom";
import { CircularProgress, Grid } from '@mui/material';
//Tooltip
import { Tooltip } from 'react-tooltip';
//import Box from '@mui/material/Box';
import configData from "../config.json";
import { Link } from 'react-router-dom';
import { getUser } from './CommonFunctions';

const AlbumDetail = () => {
    const user = getUser();
    const navigate = useNavigate();
    const { albumId } = useParams()
    //const [tableData, setTableData] = useState([])
    const [musicianList, setMusicianList] = useState([])
    const [songList, setSongList] = useState([])
    const [album, setAlbum] = useState(null);
    const [artistId, setArtistId] = useState(0);
    const [userId, setUserId] = useState(0);
    const [loading, setLoading] = useState(true);
    
    console.log("artistId " + artistId);
    
    useEffect(() => {
        console.log("ALBUM " + configData.SERVER_URL + 'Album/GetAlbumByAlbumId/' + albumId);
        fetch(configData.SERVER_URL + 'Album/GetAlbumByAlbumId/' + albumId)
            .then((data) => data.json())
            .then((data) => {
                setAlbum(data[0]);
                setUserId(data[0].userId);
                setMusicianList(data[0].musicians);
                setSongList(data[0].songs);
                setArtistId(data[0].artistId);
                setLoading(false);
            });

    }, [albumId])

    const handleNavigate = (event: MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        navigate('/albumAddEdit/' + albumId + '/' + artistId + '/' + album!["artistName"]);
    }

    const handleShoppingCart = (event: MouseEvent<HTMLImageElement>) => {
        event.preventDefault();
        
        //Don't need to use post here since we are not sending a JSON body
        fetch(configData.SERVER_URL + 'Sales/AddToShoppingCart/' + albumId + '/' + userId)
            .then((data) => data.json())
            .then((data) => {
                alert("added to shopping cart" + data);
                //TODO: Add numer of items under image here. Or add fact that this item was already added
            });
    }
   //User can only edit the record if the current user is the owner of the record
    if (user == null || user.userId != userId) {
        const element = document.getElementById("bottomButton");
        if (element) {
            element.style.display = 'none';
        }
    }

    if (album != null) {
        //console.log("album2222 " + album["userId"] + "...." + CurrentUserId + "....." + userId)
    }

    //<a href="https://www.freepik.com/icons/information/2#uuid=8c3949a8-d38a-4ddd-9ba3-1efab98e984f">Icon by customicondesign_1</a>
    if (loading) {

        return <CircularProgress style={{ color: "gold" }} className="circular" />;
    }
    else {
        if (album != null) {
            return (
                <div className="mainPanelNoPad">
                    <div className="divBlackHeader">
                        <div className="divBlackHeaderText">
                            Album: {album['albumName']}
                        </div>
                    </div>
                    <div className="flexParent">
                        <div style={{ paddingLeft: 10, paddingBottom: 5 }}>
                            <img src={album['thumbnailUrl']} alt="Album Picture" className="iconSizeBiggerBig"></img>
                        </div>
                        <div className="flex-child-right2">

                            <Grid container className="divBottomFooter">
                                <Grid item xs={12}>
                                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Artist:</div>
                                        <div className="flex-li-right">{album['artistName']}</div></div></label>
                                </Grid>
                                <Grid item xs={12}>
                                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Label:</div>
                                        <div className="flex-li-right">{album['label']}</div></div></label>
                                </Grid>
                                <Grid item xs={12}>
                                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Release Date:</div>
                                        <div className="flex-li-right">{album['releaseDate']}</div></div></label>
                                </Grid>
                                <Grid item xs={12}>
                                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Format:</div>
                                        <div className="flex-li-right">{album['mediaTypeString']}</div></div></label>
                                </Grid>
                                <Grid item xs={12}>

                                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Condition:</div>
                                        <div className="flex-li-right">{album['conditionName']} <a id="my-anchor-element"><img src="../icons8-information-64.png" alt="Album Details" className="iconSizeSmallerNoFloat" /></a></div></div></label>
                                    <Tooltip
                                        anchorSelect="#my-anchor-element"
                                        content={album['conditionDescription']}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Genre:</div>
                                        <div className="flex-li-right">{album['genreString']}</div></div></label>
                                </Grid>
                                <Grid item xs={12}>
                                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Style:</div>
                                        <div className="flex-li-right">{album['styleString']}</div></div></label>
                                </Grid>

                            </Grid>

                        </div>
                        <div className="topMenu">
                            <div className="topMenuItemAlbumDetails" id="Logout">
                                ${parseFloat(album['price']).toFixed(2)}
                            </div>
                            <Link to='' className="" id="Profile">
                                <img src="../AddShoppingCart.png" alt="Add to Shopping Cart" title="Add to Shopping Cart" className="" onClick={handleShoppingCart} />
                            </Link>
                        </div>
                    </div>
                    <div className="divBlackHeaderNoRadius">
                        <div className="divBlackHeaderTextAlignLeft">
                            Musicians
                        </div>
                    </div>
                    <div className="divBottomFooter">
                        {
                            musicianList.map((art, MusicianId) =>
                                <ul key={MusicianId}>
                                    <li><div className="flexParent"><div className="flex-li-left2">{art['musicianName']}</div> <div className="flex-li-right2">{art['description']}</div></div></li>
                                </ul>
                            )}
                    </div>
                    <div className="divBlackHeaderNoRadius">
                        <div className="divBlackHeaderTextAlignLeft">
                            Songs
                        </div>
                    </div>
                    <div className="divBottomFooter">
                        {
                            songList.map((art, SongId) =>
                                <ul key={SongId}>
                                    <li><div className="flexParent"><div className="flex-li-left2">{art['songName']}</div> <div className="flex-li-right2">{art['description']}</div></div></li>
                                </ul>
                            )}
                    </div>
                    <div id="bottomButton">
                        <hr />
                        <div className="createDiv" >
                            <div className="">
                                {/*<Link type="submit" className="createLink" to='/albumEdit/albumId'>Edit Album</Link>*/}
                                <button type="submit" className="buttonBlack" onClick={handleNavigate}>Edit</button>
                                {/*<button onClick={this.handleCancel}>Cancel</button>*/}
                            </div>
                        </div>
                    </div>

                </div>
            )
        }
    }
}

export default AlbumDetail