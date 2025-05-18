import { useState, useEffect, ChangeEvent, FormEvent, MouseEvent } from 'react'
import { useNavigate } from "react-router-dom";
import { useParams } from 'react-router';
import { artistObj, musicianObj } from "../classes/Entities"
import { CircularProgress, Grid } from '@mui/material';
import configData from "../config.json";
import { getUser } from './CommonFunctions';
//import LiveSearch from "./LiveSearch"; 
  
const ArtistAddEdit = () => {
    const user = getUser();
    const navigate = useNavigate();
    const { artistId } = useParams();
    //const artistId = 0;
    const [artist, setArtist] = useState <artistObj>();
    const [artistName, setArtistName] = useState("");
    const [loading, setLoading] = useState(false);
    const [isPopulated, setPopulated] = useState(false);
    const [musicianList, setMusicianList] = useState<musicianObj[]>([])

    //let currentMusicianRowIndex = -1;
    //const getMusicianCurrentRowLength = () => {
    //    currentMusicianRowIndex = currentMusicianRowIndex + 1;
    //    //console.log("currentMusicianRowIndex " + (currentMusicianRowIndex));
    //    return String(currentMusicianRowIndex);
    //}
    const addMusicianRow = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        const objNew = new musicianObj();
        setMusicianRow(objNew)
    }

    const setMusicianRow = (obj: musicianObj) => {
        const rows = musicianList;
        rows.push(obj);
        currentMusicianRowIndex = musicianList.length;
        //setMusicianRowIndex(musicianList.length);
        setMusicianList(rows);
    }
    const handleSave = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setLoading(true);

        for (let x = 0; x < musicianList.length; x++) {
            musicianList[x].userId = user.userId;
        }
        if (artist) {
            artist.userId = user.userId;
            artist.musicians = musicianList;
            const resultJSON = JSON.stringify(artist);
            //console.log(resultJSON);

            if (artist.artistId > 0) {
                fetch(configData.SERVER_URL + 'Artists/UpdateArtist', {
                    method: 'PUT',
                    body: resultJSON, //JSON.stringify(this.state.artData) ,
                    headers: {
                        'Accept': 'application/json',
                        "Content-Type": "application/json"
                    },

                }).then((response) => response.json())
                    .then((responseJson) => {
                        console.log("responseJson " + responseJson);
                        setLoading(false);
                        navigate("/artistDetails/" + artist.artistId);
                    })
            }
            else {
                fetch(configData.SERVER_URL + 'Artists/AddArtist', {
                    method: 'POST',
                    body: resultJSON, //JSON.stringify(this.state.artData) ,
                    headers: {
                        'Accept': 'application/json',
                        "Content-Type": "application/json"
                    },

                }).then((response) => response.json())
                    .then((responseJson) => {
                        console.log("responseJson " + responseJson);
                        setLoading(false);
                        navigate("/artistDetails/" + responseJson);
                    })
            }
        }
       
    }

    const getArtistMetaData = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        setLoading(true);
        console.log("artistName " + artistName)
        fetch(configData.SERVER_URL + 'Artists/GetArtistMetaData/' + artistName)
            .then(response => response.json())
            .then(data => {
                setArtist(data);
                setMusicianList(data.musicians);
                setPopulated(true);
                setLoading(false);
            });
    }

    if (loading) {

        return <CircularProgress style={{ color: "gold" }} className="circular" />;
    }
    else {
        if (artistId == "0") {
            if (isPopulated) {
                return (
                    <form onSubmit={handleSave} >
                        <div className="mainPanelNoPad">
                            <div className="divBlackHeader">
                                <div className="divBlackHeaderText">
                                    {/*Album: {album['albumName']}*/}
                                    Add Artist
                                </div>
                            </div>
                            <div className="flexParent">

                                <div className="flex-child-right2">
                                    <Grid container className="divBottomFooter">
                                        <Grid item xs={12} style={{ fontSize: 16, marginBottom: 10 }}>
                                            <label htmlFor="artistName"><div className="flexParent"><div className="flex-li-left">Artist:</div>
                                                <div className="flex-li-right">
                                                    <input className="form-control" type="text" id="artistName" onChange={(e) => setArtistName(e.target.value)} defaultValue={artistName} />
                                                    <button id="getArtistMetaDataBtn" type="button" className="bottomButtons bottomButtonAdjust" onClick={getArtistMetaData}>Get</button>
                                                </div>
                                            </div></label>
                                        </Grid>
                                        <Grid item xs={12} style={{ fontSize: 16, marginBottom: 10 }}>
                                            <label htmlFor="artistDescription"><div className="flexParent"><div className="flex-li-left">Description:</div>
                                                <div className="flex-li-right">
                                                    <textarea className="form-control textareaHeight" id="artistDescription" defaultValue={artist!['description']} />
                                                </div>
                                            </div></label>
                                        </Grid>
                                    </Grid>
                                </div>
                            </div>
                            <div className="divBlackHeaderNoRadius">
                                <div className="divBlackHeaderTextAlignLeft">
                                    Musicians
                                </div>
                            </div>
                            <div className="flexParent divBottomFooter divPadLeft"><div className="flex-li-left2">Name</div>
                                <div className="flex-li-right2">Comments
                                </div>
                            </div>
                            <div className="divBottomFooter">
                                {
                                    musicianList.map((art, MusicianId) =>
                                        <ul key={MusicianId}>
                                            <li><div className="flexParent"><div className="flex-li-left2">
                                                <label htmlFor="musicianId"></label>
                                                <input type="hidden" id="musicianId" value={art["musicianId"]} />
                                                {/*<input type="hidden" id="musicianArtistId" value={art["artistId"]} />*/}
                                                <input className="form-control" type="text" id="musicianName" defaultValue={art['musicianName']} />
                                            </div>
                                                <div className="flex-li-right2">
                                                    <textarea className="form-control inputLength textareaHeight" id="musicianDescription" defaultValue={art['description']} />
                                                </div>
                                                {/*<button id={getMusicianCurrentRowLength()} className="ulButton" onClick={e => {*/}
                                                {/*    handleDeleteMuscian(e, art.musicianId);*/}
                                                {/*}}>Remove</button>*/}
                                            </div>
                                            </li>
                                            <input type="hidden" id="musicianEnd" value="musicianEnd" />
                                        </ul>
                                    )}
                                <div className="divButton">
                                    <button id="addMusicianBtn" className="bottomButtons" onClick={addMusicianRow}>Add New Musician</button>
                                </div>
                            </div>
                            <hr />
                            <div className="createDiv" >
                                <div className="">
                                    {/* <Link type="submit" className="createLink" to='/'>Save Album</Link>*/}
                                    <button type="submit" className="buttonBlack">Save</button>
                                    {/*<button onClick={this.handleCancel}>Cancel</button>*/}
                                </div>
                            </div>
                        </div>
                    </form>
                )
            }
            else {
                return (
                    <form onSubmit={handleSave} >
                        <div className="mainPanelNoPad">
                            <div className="divBlackHeader">
                                <div className="divBlackHeaderText">
                                    {/*Album: {album['albumName']}*/}
                                    Add Artist
                                </div>
                            </div>
                            <div className="flexParent">

                                <div className="flex-child-right2">
                                    <Grid container className="divBottomFooter">
                                        <Grid item xs={12} style={{ fontSize: 16, marginBottom: 10 }}>
                                            <label htmlFor="artistName"><div className="flexParent"><div className="flex-li-left">Artist:</div>
                                                <div className="flex-li-right">
                                                    <input className="form-control" type="text" id="artistName" onChange={(e) => setArtistName(e.target.value)} />
                                                    <button id="getArtistMetaDataBtn" type="button" className="bottomButtons bottomButtonAdjust" onClick={getArtistMetaData}>Get Information</button>
                                                </div>
                                            </div></label>
                                        </Grid>
                                    </Grid>
                                </div>
                            </div>
                        </div>
                    </form>
                )
            }
        }
    }
}

export default ArtistAddEdit;