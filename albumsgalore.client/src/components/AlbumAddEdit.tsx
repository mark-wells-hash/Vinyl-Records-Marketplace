import { useState, useEffect, ChangeEvent, FormEvent, MouseEvent } from 'react'
import { useNavigate } from "react-router-dom";
import { useParams } from 'react-router';
import { CircularProgress, Grid } from '@mui/material';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';
//import ListItemAvatar from '@mui/material/ListItemAvatar';
import Checkbox from '@mui/material/Checkbox';
//import Avatar from '@mui/material/Avatar';
import configData from "../config.json";
//import { Link } from 'react-router-dom';
import { getUser } from './CommonFunctions';
import LiveSearch from "./LiveSearch";
import { musicianObj, songObj, albumMusicians, albumGenres, albumStyles, albumMediaTypes } from "../classes/Entities"

const AlbumEdit = () => {
    const user = getUser();
    const navigate = useNavigate(); 
    const { albumId, artistId, artistName } = useParams();
    console.log("album = " + albumId + " artist = " + artistId + " artistName = " + artistName);

    const [musicianList, setMusicianList] = useState<musicianObj[]>([])
    const [mediaList, setMediaList] = useState([])
    const [statusList, setStatusList] = useState([])
    const [conditionList, setConditionList] = useState([])
    const [genreList, setGenreList] = useState([])
    const [styleList, setStyleList] = useState([])
    const [songList, setSongList] = useState<songObj[]>([])
    const [album, setAlbum] = useState(null);
    const [albumName, setAlbumName] = useState("");
    const [isPopulated, setPopulated] = useState(false);
    //const [albumId, setArtistId] = useState(0);
    const [loading, setLoading] = useState(true);
    const [checkedGenre, setCheckedGenre] = useState<number[]>([]);
    const [checkedGenreID, setCheckedGenreID] = useState<number[]>([]);
    const [checkedStyle, setCheckedStyle] = useState<number[]>([]);
    const [checkedMedia, setCheckedMedia] = useState<number[]>([]);
    const [albumCondition, setAlbumCondition] = useState(0);
    const [albumStatus, setAlbumStatus] = useState(0);
    const [songRowIndex, setSongRowIndex] = useState(-1);
    const [musicianRowIndex, setMusicianRowIndex] = useState(-1); // ***NOTE: Removing this since setting it on getMusicianCurrentRowLength causes multiple renders
    const [results, setResults] = useState<{ musicianId: string; musicianName: string; description: string }[]>();
    const [selectedProfile, setSelectedProfile] = useState<{
        musicianId: string;
        musicianName: string;
        description: string;
    }>();

    let currentMusicianRowIndex = -1;
    let currentSongRowIndex = -1;
    const getMusicianCurrentRowLength = () => {
        currentMusicianRowIndex = currentMusicianRowIndex + 1;
        //console.log("currentMusicianRowIndex " + (currentMusicianRowIndex));
        return String(currentMusicianRowIndex);
    }
    const addMusicianRow = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        const objNew = new musicianObj();
        setMusicianRow(objNew)
    }

    const setMusicianRow = (obj: musicianObj) => {
        const rows = musicianList;
        rows.push(obj);
        currentMusicianRowIndex = musicianList.length;
        console.log("rowIndex " + musicianRowIndex);
        setMusicianRowIndex(musicianList.length);
        setMusicianList(rows);
    }

    type changeHandler = React.ChangeEventHandler<HTMLInputElement>;
    const handleChange: changeHandler = (e) => {
        const { target } = e;
        //console.log("handleChange " + target.value)
        if (!target.value.trim()) return setResults([]);

        fetch(configData.SERVER_URL + 'Artists/GetMusiciansByMusicianName/' + target.value.trim())
            .then((data) => data.json())
            .then((data) => {
                setResults(data);
                //console.log("Check how many times this gets called " + data[0].musicianId);
                setLoading(false);
            });
    };

    const handleMusicianUpdateName = (e: React.ChangeEventHandler<HTMLInputElement>, id: number) => {
        const { target } = e;
        //console.log("handleChange " + target.value + " : " + target.name + " : " + id)
        if (id == 0) {
            const rows = musicianList;
            const obj = rows[parseInt(target.name)];
            console.log(obj);
            const objNew = new musicianObj();
            objNew.musicianId = id;
            objNew.musicianName = target.value;
            objNew.description = obj.description;
            console.log(objNew);
            rows.splice(parseInt(target.name), 1, objNew);
            setMusicianList(rows);
        }    
    };

    const handleMusicianUpdateDescription = (e: React.ChangeEventHandler<HTMLInputElement>, id: number) => {
        const { target } = e;
        //console.log("handleChange " + target.value + " : " + target.name + " : " + id)
        if (id == 0) {
            const rows = musicianList;
            const obj = rows[parseInt(target.name)];
            console.log(obj);
            const objNew = new musicianObj();
            objNew.musicianId = id;
            objNew.musicianName = obj.musicianName;
            objNew.description = target.value;
            console.log(objNew);
            rows.splice(parseInt(target.name), 1, objNew);
            setMusicianList(rows);
        }
    };

    const handleSongUpdateName = (e: React.ChangeEventHandler<HTMLInputElement>, id: number) => {
        const { target } = e;
        //console.log("handleChange " + target.value + " : " + target.name + " : " + id)
        if (id == 0) {
            const rows = songList;
            const obj = rows[parseInt(target.name)];
            console.log(obj);
            const objNew = new songObj();
            objNew.songId = id;
            objNew.songName = target.value;
            objNew.description = obj.description;
            console.log(objNew);
            rows.splice(parseInt(target.name), 1, objNew);
            setSongList(rows);
        }
    };

    const handleSongUpdateDescription = (e: React.ChangeEventHandler<HTMLInputElement>, id: number) => {
        const { target } = e;
        //console.log("handleChange " + target.value + " : " + target.name + " : " + id)
        if (id == 0) {
            const rows = songList;
            const obj = rows[parseInt(target.name)];
            console.log(obj);
            const objNew = new songObj();
            objNew.songId = id;
            objNew.songName = obj.songName;
            objNew.description = target.value;
            console.log(objNew);
            rows.splice(parseInt(target.name), 1, objNew);
            setSongList(rows);
        }
    };

    if (selectedProfile != null && selectedProfile.musicianId != '') {
        //console.log("what is selectedProfile at this time: " + selectedProfile.musicianName);
        const objNew = new musicianObj();
        objNew.musicianId = parseInt(selectedProfile.musicianId);
        objNew.musicianName = selectedProfile.musicianName;
        objNew.description = selectedProfile.description;
        setMusicianRow(objNew);

        const newMusician = {
            musicianId: '',
            musicianName: '',
            description: ''
        };
        setSelectedProfile(newMusician);
    }

    const getSongCurrentRowLength = () => {
        currentSongRowIndex = currentSongRowIndex + 1;
        //console.log("currentSongRowIndex " + (currentSongRowIndex));
        return String(currentSongRowIndex);
    }

    const addSongRow = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        //console.log("rowIndex " + songRowIndex);
        const objNew = new songObj();
        const rows = songList;
        rows.push(objNew);
        currentSongRowIndex = songList.length;
        console.log("rowIndex " + songRowIndex);
        setSongRowIndex(songList.length);
        setSongList(rows);
    }

    const onSelectChange = (event: ChangeEvent<HTMLSelectElement>) => {
        const value: string | number = event.target.value;
        const numberResult = parseInt(value);
       // console.log("SelectChange: " + event.target.id + " = " + numberResult)
        if (event.target.id === "albumCondition") {
            setAlbumCondition(numberResult)
        }
        if (event.target.id === "albumStatus") {
            setAlbumStatus(numberResult)
        }
    };

    const handleToggleGenre = (value: number, id: number) => () => {
        const currentIndex = checkedGenre.indexOf(value);
        const currentIdIndex = checkedGenreID.indexOf(id);
        const newChecked = [...checkedGenre];
        const newCheckedId = [...checkedGenreID];
        console.log("ad on change: " + id);
        if (currentIndex === -1) {
            newChecked.push(value);
            newCheckedId.push(id);
        } else {
            newChecked.splice(currentIndex, 1);
            newCheckedId.splice(currentIdIndex, 1);
        }
        setCheckedGenreID(newCheckedId);
        setCheckedGenre(newChecked);
    };

    const handleToggleMedia = (value: number) => () => {
        const currentIndex = checkedMedia.indexOf(value);
        const newChecked = [...checkedMedia];
        if (currentIndex === -1) {
            newChecked.push(value);
        } else {
            newChecked.splice(currentIndex, 1);
        }

        setCheckedMedia(newChecked);
    };

    const handleToggleStyle = (value: number) => () => {
        const currentIndex = checkedStyle.indexOf(value);
        const newChecked = [...checkedStyle];
        if (currentIndex === -1) {
            newChecked.push(value);
        } else {
            newChecked.splice(currentIndex, 1);
        }

        setCheckedStyle(newChecked);
    };
    
    useEffect(() => {
        console.log("ALBUM " + albumId);
        if (albumId != '0') {

            fetch(configData.SERVER_URL + 'Album/GetAlbumByAlbumId/' + albumId)
                .then((data) => data.json())
                .then((data) => {
                    console.log("Check how many times this gets called in AlbumDetail: " + JSON.stringify(data[0].artistName));
                    setAlbum(data[0]);
                    setAlbumName(data[0].albumName);
                    setSongList(data[0].songs);
                    setMusicianList(data[0].musicians);
                    setAlbumCondition(data[0].albumConditionTypeId);
                    setAlbumStatus(data[0].albumStatusId);

                    const newCheckedGenre = [...checkedGenre];
                    const newCheckedGenreID = [...checkedGenreID];
                    //for (let x = 0; x < data[0].genres.length; x++) {
                    //    newCheckedGenre.push(data[0].genres[x].genreId);
                    //    //newCheckedGenreID.push(data[0].genre[x].albumGenreId);
                    //}
                    for (let x = 0; x < data[0].albumGenres.length; x++) {
                        newCheckedGenre.push(data[0].albumGenres[x].genreId);
                        newCheckedGenreID.push(data[0].albumGenres[x].albumGenreId);
                    }
                    setCheckedGenre(newCheckedGenre);
                    setCheckedGenreID(newCheckedGenreID);

                    const newCheckedStyle = [...checkedStyle];
                    for (let x = 0; x < data[0].albumStyles.length; x++) {
                        newCheckedStyle.push(data[0].albumStyles[x].styleId);
                    }
                    setCheckedStyle(newCheckedStyle);

                    const newCheckedMedia = [...checkedMedia];
                    for (let x = 0; x < data[0].albumMediaTypes.length; x++) {
                        newCheckedMedia.push(data[0].albumMediaTypes[x].mediaTypeId);
                    }
                    setCheckedMedia(newCheckedMedia);

                    setLoading(false);
                });
        }
        fetch(configData.SERVER_URL + 'Album/GetMediaList')
            .then(response => response.json())
            .then(data => {
                setMediaList(data);
            });
        fetch(configData.SERVER_URL + 'Album/GetAlbumConditionTypes')
            .then(response => response.json())
            .then(data => {
                setConditionList(data);
            });
        fetch(configData.SERVER_URL + 'Album/GetAlbumStatusTypes')
            .then(response => response.json())
            .then(data => {
                setStatusList(data);
            });
        fetch(configData.SERVER_URL + 'Album/GetGenres')
            .then(response => response.json())
            .then(data => {
                console.log(data[0]["genreId"] + " : " + data[0].albumGenreId);
                setGenreList(data);
            });
        fetch(configData.SERVER_URL + 'Album/GetStyles')
            .then(response => response.json())
            .then(data => {
                setStyleList(data);
            });
        setLoading(false);
    }, [])

    const getAlbumMetaData = (e: MouseEvent<HTMLButtonElement>) => {
        e.preventDefault();
        setLoading(true);
        console.log("albumName " + albumName + " artistName " + artistName)
        //console.log("BEFORE GET META " + album!["albumId"]);
        fetch(configData.SERVER_URL + 'Album/GetAlbumMetaData/' + albumName + '/' + artistName)
            .then(response => response.json())
            .then(data => {
                //data.albumId = album!["albumId"];
                setAlbum(data);
                setSongList(data.songs);
                setMusicianList(data.musicians);
                setPopulated(true);
                setLoading(false);
            });
        
    }
    const handleSave = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        setLoading(true);
        const target = event.currentTarget;
        const musicianArray: unknown[] = [];
        const songArray: songObj[] = [];
        const albumMusiciansArray: unknown[] = [];
        const albumGenresArray: unknown[] = [];
        const albumMediaTypesArray: unknown[] = [];
        const albumStylesArray: unknown[] = [];
        const musician = new musicianObj();
        const song = new songObj();

        const _album = {
            albumId: 0,
            albumName: "",
            description: "",
            label: "",
            releaseDate: "",
            price: 0.0,
            thumbnailUrl: "",
            coverImageUrl: "",
            albumConditionTypeId: 0,
            albumStatusId: 0,
            albumGenres: albumGenresArray,
            albumStyles: albumStylesArray,
            albumMediaTypes: albumMediaTypesArray,
            artistId: 0,
            artistName: "",
            albumMusicians: albumMusiciansArray, 
            musicians: musicianArray,
            songs: songArray,
            userId: 0
        }
        
        for (let i = 0; i < target.length; i++) {

            const albumResult = target[i].id.includes("album");
            const musicianResult = target[i].id.includes("musician");
            const songResult = target[i].id.includes("song");
            // as HTMLInputElement
            const inputTarget = target[i] as HTMLInputElement;
            const value = inputTarget.value;

            if (albumResult || target[i].id == "" || target[i].id == "artistId") {

                if (target[i].id == "albumName") {
                    _album.albumName = value != null ? value : "";
                    _album.userId = user.userId;
                    //Create these objects only once during the "albumName"" target
                    console.log("checkedGenre.length " + checkedGenre.length)
                    for (let x = 0; x < checkedGenre.length; x++) {
                        //console.log("creating new obj");
                        const objNew = new albumGenres();
                        objNew.albumGenreId = checkedGenreID[x];
                        objNew.albumId = album!["albumId"];
                        objNew.genreId = checkedGenre[x];
                        objNew.genre = genreList[0];
                        albumGenresArray.push(objNew);
                    }
                    for (let x = 0; x < checkedStyle.length; x++) {
                        const objNew = new albumStyles();
                        objNew.albumStyleId = 0;
                        objNew.albumId = album!["albumId"];
                        objNew.styleId = checkedStyle[x];
                        objNew.style = styleList[0];
                        albumStylesArray.push(objNew);
                    }
                    for (let x = 0; x < checkedMedia.length; x++) {
                        const objNew = new albumMediaTypes();
                        objNew.albumMediaTypeId = 0;
                        objNew.albumId = album!["albumId"];
                        objNew.mediaTypeId = checkedMedia[x];
                        objNew.mediaType = mediaList[0];
                        albumMediaTypesArray.push(objNew);
                    }
                }
                if (target[i].id == "albumDescription") {
                    //console.log("albumDescription " + target[i].value)
                    _album.description = value != null ? value : "";
                }
                if (target[i].id == "albumLabel") {
                    _album.label = value != null ? value : "";
                }
                if (target[i].id == "albumReleaseDate") {
                    //console.log("Hmmmm " + target[i].getAttribute("value"));
                    _album.releaseDate = value != null ? value : "";
                }
                if (target[i].id == "albumPrice") {
                    const newValue: string | number = value != null ? value : "";
                    const numberResult = parseFloat(newValue);
                    _album.price = numberResult;
                }

                _album.artistName = album!['artistName'];
                _album.artistId = artistId; // album!["artistId"];
                _album.albumId = album!["albumId"];
                _album.thumbnailUrl = album!["thumbnailUrl"];
                _album.coverImageUrl = album!["coverImageUrl"];
                _album.albumConditionTypeId = albumCondition;
                _album.albumStatusId = albumStatus;
                _album.albumMusicians = albumMusiciansArray;
                _album.albumGenres = albumGenresArray;
                _album.albumStyles = albumStylesArray;
                _album.albumMediaTypes = albumMediaTypesArray;
               // _album.musicians = musicianArray;
               // _album.songs = songArray;
            }   
           

            if (musicianResult) {
                if (target[i].id == "musicianId") {
                    if (target[i].getAttribute("value") == "") {
                        musician.musicianId = 0;
                    }
                    else {
                        const newValue: string | number = value != null ? value : "";
                        const numberResult = parseInt(newValue);
                        musician.musicianId = numberResult;
                    }
                    musician.userId = user.userId;

                    const objNew = new albumMusicians();
                    objNew.albumMusiciansId = 0;
                    objNew.albumId = album!["albumId"];
                    objNew.musicianId = musician.musicianId
                    objNew.musician = musicianList[0];
                    albumMusiciansArray.push(objNew);
                }
                console.log("Adding Musician " + target[i].id);
                if (target[i].id == "musicianArtistId") {
                    if (target[i].getAttribute("value") == "") {
                        musician.artistId = 0;
                    }
                    else {
                        musician.artistId = album!['artistId'];
                        musician.albumId = album!['artistId'];
                    }
                }
                if (target[i].id == "musicianName") {
                    musician.musicianName = value != null ? value : "";
                    console.log("Adding Musician Name " + musician.musicianName);
                }
                if (target[i].id == "musicianDescription") {
                    musician.description = value != null ? value : "";
                }
                if (target[i].id == "musicianEnd") {
                    //console.log("at musician end " + musician.musicianName);
                    if (musician.musicianName == "") {
                        //break;
                    }
                    else {
                        console.log("SETTING MUSICIAN ARRAY " + musician.musicianName)
                        const ResultMusician = {
                            musicianId: musician.musicianId,
                            musicianName: musician.musicianName,
                            description: musician.description,
                            artistId: musician.artistId,
                            albumId: musician.albumId,
                            userId: musician.userId
                        }
                        musicianArray.push(ResultMusician);
                    }
                }
            }
            //Songs
            if (songResult) {
                if (target[i].id == "songId") {
                    if (target[i].getAttribute("value") == "") {
                        song.songId = 0;
                    }
                    else {
                        const newValue: string | number = value != null ? value : "";
                        const numberResult = parseInt(newValue)
                        song.songId = numberResult;
                    }
                    song.userId = user.userId;
                }
                if (target[i].id == "songName") {
                    song.songName = value != null ? value : "";
                }
                if (target[i].id == "songDescription") {
                    song.description = value != null ? value : "";
                }
                if (target[i].id == "songEnd") {
                    //console.log("at song end " + song.songName);
                    if (song.songName == "") {
                        //break;
                    }
                    else {
                        //console.log("SETTING SONG ARRAY " + song.songName)
                        const ResultSong = {
                            songId: song.songId,
                            songName: song.songName,
                            description: song.description,
                            albumId: album!["albumId"],
                            userId: song.userId
                        }
                        songArray.push(ResultSong);
                    }
                }
            }

            
        }
        _album.musicians = musicianArray;
        _album.songs = songArray;
        const resultJSON = JSON.stringify(_album);
        console.log(resultJSON);
        
        // PUT request for Edit Artist.
        if (album!["albumId"] > 0) {
            fetch(configData.SERVER_URL + 'Album/UpdateAlbum', {
                    method: 'PUT',
                    body: resultJSON, 
                    headers: {
                        'Accept': 'application/json',
                        "Content-Type": "application/json" 
                    },

                }).then((response) => response.json())
                    .then((responseJson) => {
                        console.log("responseJson " + responseJson);
                        setLoading(false);
                        navigate("/albumDetail/" + album!["albumId"]);
                    })
        }
        else {
            fetch(configData.SERVER_URL + 'Album/AddAlbum', {
                method: 'POST',
                body: resultJSON, 
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                },

            }).then((response) => response.json())
                .then((responseJson) => {
                    console.log("responseJson " + responseJson);
                    setLoading(false);
                    navigate("/albumDetail/" + responseJson);
                })
        }
    }

    const handleDeleteMuscian = (event: MouseEvent<HTMLButtonElement>, id: number) => {
        event.preventDefault();
        console.log("After GET META " + album!["albumId"]);
        if (!window.confirm("Do you want to delete musician with Id: " + id + " and target " + event.currentTarget.id))
            return;
        else {

            if (id === 0) {
                const musicianArray = [];
                const row = musicianList;
                for (let i = 0; i < row.length; i++) {
                    console.log((event.currentTarget.id) + " == " + (i + 1));
                    if (i != parseInt(event.currentTarget.id)) {
                        console.log("Adding " + row[i].musicianName);
                        musicianArray.push(row[i]);
                    }
                    else {
                        console.log("leaving out " + row[i].musicianName);
                    }
                }

                currentMusicianRowIndex = musicianArray.length;
                //Calling this set method to invoke a change so it re-renders as expected
                setMusicianRowIndex(musicianArray.length);
                setMusicianList(musicianArray);
            }
            else {
                fetch(configData.SERVER_URL + 'Album/DeleteAlbumMusician/' + id + '/' + album!["albumId"], {
                    method: 'delete'
                }).then//(data => 
                {
                    //if (data)
                    console.log(musicianList);
                    const musicianArray = [];
                    const row = musicianList;
                    
                    for (let i = 0; i < row.length; i++) {

                        if (row[i].musicianId != id) {
                            console.log("adding " + row[i]);
                            musicianArray.push(row[i]);
                        }
                        else {
                            console.log("leaving out " + row[i]);
                        }
                    }

                    setMusicianList(musicianArray);
                }
            }
        }
    }

    const handleDeleteSong = (event: MouseEvent<HTMLButtonElement>, id: number) => {
        event.preventDefault();
        //, id: number
        if (!window.confirm("Do you want to delete song with Id: " + id + " and target " + event.currentTarget.id))
            return;
        else {
            console.log(id);
            if (id === 0) {
                //console.log(currentSongRowIndex);
                const songArray = [];
                const row = songList;
                for (let i = 0; i < row.length; i++) {
                    //console.log((event.currentTarget.id) + " == " + i);
                    if (i != parseInt(event.currentTarget.id)) {
                        console.log("Adding " + row[i].songName);
                        const objNew = new songObj();
                        objNew.songId = row[i].songId;
                        objNew.songName = row[i].songName;
                        objNew.description = row[i].description; 
                        songArray.push(objNew);
                    }
                    else {
                        console.log("leaving out " + row[i].songName);
                    }
                }

                currentSongRowIndex = songArray.length;
                setSongRowIndex(songArray.length);
                setSongList(songArray);
            }
            else {
                fetch(configData.SERVER_URL + 'Album/DeleteSong/' + id, {
                    method: 'delete'
                }).then//(data =>
                {
                    const songArray = [];
                    const row = songList;
                    for (let i = 0; i < row.length; i++) {

                        if (row[i].songId != id) {
                            const objNew = new songObj();
                            objNew.songId = row[i].songId;
                            objNew.songName = row[i].songName;
                            objNew.description = row[i].description;
                            songArray.push(objNew);
                        }
                        else {
                            console.log("leaving out " + row[i].songName);
                        }
                    }

                    setSongRowIndex(songArray.length);
                    setSongList(songArray);
                }
            }
        }
    }


    //<a href="https://www.freepik.com/icons/information/2#uuid=8c3949a8-d38a-4ddd-9ba3-1efab98e984f">Icon by customicondesign_1</a>
    if (loading) {

        return <CircularProgress style={{ color: "gold" }} className="circular" />;
    }
    else {
        if (album != null) {
            return (
                <form onSubmit={handleSave} >
                    <div className="mainPanelNoPad">
                        <div className="divBlackHeader">
                            <div className="divBlackHeaderText">
                                Album: {album['albumName']}
                            </div>
                        </div>
                        <div className="flexParent">
                            
                            <div className="flex-child-right2">
                                
                                <Grid container className="divBottomFooter">
                                    <Grid item xs={12} style={{ fontSize: 16, marginBottom: 10 }}>
                                        <label htmlFor="artistName"><div className="flexParent"><div className="flex-li-left">Artist:</div>
                                            <div className="flex-li-right">
                                                <label className="form-control" style={{ fontSize: 17}}  id="artistName" defaultValue={album['artistName']}>{album['artistName']}</label>
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <label htmlFor="albumName"><div className="flexParent"><div className="flex-li-left">Album:</div>
                                            <div className="flex-li-right">
                                                <input className="form-control" type="text" id="albumName" onChange={(e) => setAlbumName(e.target.value)} defaultValue={album['albumName']} />
                                                <button id="getArtistMetaDataBtn" type="button" className="bottomButtons bottomButtonAdjust2" onClick={getAlbumMetaData}>Get Information</button>
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <label htmlFor="albumName"><div className="flexParent"><div className="flex-li-left">Comments:</div>
                                            <div className="flex-li-right">
                                                <textarea className="form-control" id="albumDescription" defaultValue={album['description']} />
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <label htmlFor="albumLabel"><div className="flexParent"><div className="flex-li-left">Label:</div>
                                            <div className="flex-li-right">
                                                <div className="flexParent">
                                                    <div className="flex-li-left">
                                                        <input className="form-control" type="text" id="albumLabel" defaultValue={album['label']} />
                                                    </div>
                                                    <div className="flex-info-right">
                                                        <label className="whiteTextSmallerSize">* Auto populated if available</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <label htmlFor="albumReleaseDate"><div className="flexParent"><div className="flex-li-left">Release Date:</div>
                                            <div className="flex-li-right">
                                                <div className="flexParent">
                                                    <div className="flex-li-left">
                                                        <input className="form-control" type="text" id="albumReleaseDate" defaultValue={album['releaseDate']} />
                                                    </div>
                                                    <div className="flex-info-right">
                                                        <label className="whiteTextSmallerSize">* Auto populated if available</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <label htmlFor="albumCondition"><div className="flexParent"><div className="flex-li-left">Condition:</div>
                                            <div className="flex-li-right">
                                                <select className="form-control selectBox" data-val="true" id="albumCondition" defaultValue={album['albumConditionTypeId']} onChange={onSelectChange}>
                                                    {conditionList?.map(condition =>
                                                        <option className="selectBoxItem" key={condition['albumConditionTypeId']} value={condition['albumConditionTypeId']} >
                                                            {condition['conditionName']}
                                                        </option>
                                                    )}
                                                </select>

                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <label htmlFor="albumStatus"><div className="flexParent"><div className="flex-li-left">Status:</div>
                                            <div className="flex-li-right">
                                                <select className="form-control selectBox" data-val="true" id="albumStatus" defaultValue={album['albumStatusId']} onChange={onSelectChange}>
                                                    {statusList?.map(status =>
                                                        <option className="selectBoxItem" key={status['albumStatusId']} value={status['albumStatusId']}>
                                                            {status['statusName']}
                                                        </option>
                                                    )}
                                                </select>
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <label htmlFor="albumPrice"><div className="flexParent"><div className="flex-li-left">Price ($):</div>
                                            <div className="flex-li-right">
                                                <input className="form-control" type="text" id="albumPrice" defaultValue={parseFloat(album['price']).toFixed(2)} />
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12} sx={{ paddingBottom: 0 }}>
                                        <label htmlFor=""><div className="flexParent"><div className="flex-li-left">Format:</div>
                                            <div className="flex-li-right">
                                                <List dense sx={{ width: '100%', minWidth: 360, paddingRight: 3, paddingBottom: 0, marginBottom: 2, borderRadius: 1, bgcolor: 'rgb(232, 240, 254)', color: 'black' }} className="muiListContainer">
                                                    {mediaList?.map(media => {
                                                        const labelId = `checkbox-list-secondary-label-${media['mediaTypeId']}`;
                                                        return (
                                                            <ListItem
                                                                key={media['mediaTypeId']}
                                                                className="muiItem"
                                                                sx={{
                                                                    '& .MuiListItemSecondaryAction-root': {
                                                                        height: '50%',
                                                                        top: '15%'
                                                                    },
                                                                    '& MuiList-root': {
                                                                        paddingBottom: 0,
                                                                    }
                                                                }}
                                                                secondaryAction={
                                                                    <Checkbox
                                                                        edge="end"
                                                                        onChange={handleToggleMedia(media['mediaTypeId'])}
                                                                        checked={checkedMedia.includes(media['mediaTypeId'])} 
                                                                        inputProps={{ 'aria-labelledby': labelId }}
                                                                    />
                                                                }
                                                                disablePadding
                                                            >
                                                                <ListItemButton onClick={handleToggleMedia(media['mediaTypeId'])}>
                                                                    {/*<ListItemAvatar>*/}
                                                                    {/*    <Avatar*/}
                                                                    {/*        alt={`Avatar n°${media['mediaId'] + 1}`}*/}
                                                                    {/*        src={`/static/images/avatar/${media['mediaId'] + 1}.jpg`}*/}
                                                                    {/*    />*/}
                                                                    {/*</ListItemAvatar>*/}
                                                                    <ListItemText id={labelId} primary={`${media['mediaName']}`} />
                                                                </ListItemButton>
                                                            </ListItem>
                                                        );
                                                    })}
                                                </List>
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12} sx={{ paddingBottom: 0 }} >
                                        <label htmlFor=""><div className="flexParent"><div className="flex-li-left">Genre:</div>
                                            <div className="flex-li-right">
                                                <List dense sx={{ width: '100%', minWidth: 360, paddingRight: 3, paddingBottom: 0, marginBottom: 2, borderRadius: 1, bgcolor: 'rgb(232, 240, 254)', color: 'black' }} className="muiListContainer">
                                                    {genreList?.map(genre => {
                                                        const labelId = `checkbox-list-secondary-label-${genre['genreId']}`;
                                                        return (
                                                            <ListItem
                                                                key={genre['genreId']}
                                                                className="muiItem"
                                                                sx={{
                                                                    '& .MuiListItemSecondaryAction-root': {
                                                                        height: '50%',
                                                                        top: '15%'
                                                                    },
                                                                    '& MuiList-root': {
                                                                        paddingBottom: 0,
                                                                    }
                                                                }}
                                                                secondaryAction={
                                                                    <Checkbox
                                                                        edge="end"
                                                                        onChange={handleToggleGenre(genre['genreId'], genre['albumGenreId'])}
                                                                        checked={checkedGenre.includes(genre['genreId'])} inputProps={{ 'aria-labelledby': labelId }}
                                                                    />
                                                                }
                                                                disablePadding
                                                            >
                                                                <ListItemButton onClick={handleToggleGenre(genre['genreId'], genre['albumGenreId'])}>
                                                                    {/*<ListItemAvatar>*/}
                                                                    {/*    <Avatar*/}
                                                                    {/*        alt={`Avatar n°${genre['genreId'] + 1}`}*/}
                                                                    {/*        src={`/static/images/avatar/${genre['genreId'] + 1}.jpg`}*/}
                                                                    {/*    />*/}
                                                                    {/*</ListItemAvatar>*/}
                                                                    <ListItemText id={labelId} primary={`${genre['genreName']}`} />
                                                                </ListItemButton>
                                                            </ListItem>
                                                        );
                                                    })}
                                                </List>
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <label htmlFor=""><div className="flexParent"><div className="flex-li-left">Style:</div>
                                            <div className="flex-li-right">
                                                <List dense sx={{ width: '100%', minWidth: 360, paddingRight: 3, paddingBottom: 0, bgcolor: 'rgb(232, 240, 254)', color: 'black' }} className="muiListContainer">
                                                    {styleList?.map(style => {
                                                        const labelId = `checkbox-list-secondary-label-${style['styleId']}`;
                                                        return (
                                                            <ListItem
                                                                key={style['styleId']}
                                                                className="muiItem"
                                                                sx={{
                                                                    '& .MuiListItemSecondaryAction-root': {
                                                                        height: '50%',
                                                                        top: '15%'
                                                                    },
                                                                    '& MuiList-root': {
                                                                        paddingBottom: 0,
                                                                    }
                                                                }}
                                                                secondaryAction={
                                                                    <Checkbox
                                                                        edge="end"
                                                                        onChange={handleToggleStyle(style['styleId'])}
                                                                        checked={checkedStyle.includes(style['styleId'])}
                                                                        inputProps={{ 'aria-labelledby': labelId }}
                                                                    />
                                                                }
                                                                disablePadding
                                                            >
                                                                <ListItemButton onClick={handleToggleStyle(style['styleId'])}>
                                                                    {/*<ListItemAvatar>*/}
                                                                    {/*    <Avatar*/}
                                                                    {/*        alt={`Avatar n°${genre['genreId'] + 1}`}*/}
                                                                    {/*        src={`/static/images/avatar/${genre['genreId'] + 1}.jpg`}*/}
                                                                    {/*    />*/}
                                                                    {/*</ListItemAvatar>*/}
                                                                    <ListItemText id={labelId} primary={`${style['styleName']}`} />
                                                                </ListItemButton>
                                                            </ListItem>
                                                        );
                                                    })}
                                                </List>
                                            </div>
                                        </div></label>
                                    </Grid>
                                </Grid>
                            </div>
                            <div className="adjustAlbumImageEdit">
                            
                                {/*<div style={{ paddingLeft: 10, paddingBottom: 5 }}>*/}
                                <img src={album['thumbnailUrl']} alt="Album Picture" className="iconSizeBiggerBig"></img>
                                <label className="whiteTextSmallerSize">&nbsp;* Auto populated if available</label>
                                {/*</div>*/}
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
                                        <li key={Math.floor(Math.random() * 1000)}>
                                            <div className="flexParent"><div className="flex-li-left2">
                                            <label htmlFor="musicianId"></label>
                                                <input type="hidden"  id="musicianId" value={art["musicianId"]} />
                                                <input type="hidden"  id="musicianArtistId" value={art["artistId"]} />
                                                <input className="form-control" type="text" name={String(currentMusicianRowIndex + 1)} id="musicianName" onChange={e => { handleMusicianUpdateName(e, art.musicianId) }} defaultValue={art['musicianName']} />
                                        </div>
                                            <div className="flex-li-right2">
                                                    <input className="form-control inputLength" type="text" name={String(currentMusicianRowIndex + 1)} id="musicianDescription" onChange={e => { handleMusicianUpdateDescription(e, art.musicianId) }} defaultValue={art['description']} />
                                            </div>
                                            <button id={getMusicianCurrentRowLength()} className="ulButton" onClick={e => {
                                                handleDeleteMuscian(e, art.musicianId);
                                            }}>Remove</button>
                                        </div>
                                        </li>
                                        <input type="hidden" id="musicianEnd" value="musicianEnd" />
                                    </ul>
                                )}
                            <div className="divButton">
                                <button id="addMusicianBtn" className="bottomButtons" onClick={addMusicianRow}>Add New Musician</button>
                                <div className="musicianSearch">
                                <LiveSearch
                                    results={results}
                                    value={selectedProfile?.musicianName}
                                    renderItem={(item) => <p>{item.musicianName}</p>}
                                    onChange={handleChange}
                                    onSelect={(item) => setSelectedProfile(item)}
                                    placeholder="Find existing musician"
                                    widthIn="200px"
                                    heightIn="32px"
                                    />
                                </div>
                            </div>
                        </div>
                        <div className="divBlackHeaderNoRadius">
                            <div className="divBlackHeaderTextAlignLeft">
                                Songs
                            </div>
                        </div>
                        <div className="flexParent divBottomFooter divPadLeft"><div className="flex-li-left2">Name</div><div className="flex-li-right2">Comments</div></div>
                        <div className="divBottomFooter">
                            {
                                songList.map((art, SongId) =>
                                    <ul key={SongId} >
                                        <li key={Math.floor(Math.random() * 1000)} >
                                            <input type="hidden" id="songId" value={art["songId"]} />
                                            <div className="flexParent"><div className="flex-li-left2">
                                                <input className="form-control" type="text" name={String(currentSongRowIndex + 1)} id="songName" onChange={e => { handleSongUpdateName(e, art.songId) }} defaultValue={art['songName']} />
                                            </div>
                                                <div className="flex-li-right2"><input name={String(currentSongRowIndex + 1)} className="form-control inputLength" type="text" id="songDescription" onChange={e => { handleSongUpdateDescription(e, art.songId) }} defaultValue={art['description']} />
                                                </div>
                                                <button id={getSongCurrentRowLength()} className="ulButton" onClick={e => {
                                                    handleDeleteSong(e, art.songId);
                                                }}>Remove</button>
                                            </div>
                                        </li>
                                        <input type="hidden" id="songEnd" value="songEnd" />
                                    </ul>
                                )}
                            <div className="divButton">
                                <button id="addSongBtn" className="bottomButtons" onClick={addSongRow}>Add Song</button>
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
                </form >
            )
        }
        else {
            return (
                <form onSubmit={handleSave} >
                    <div className="mainPanelNoPad">
                        <div className="divBlackHeader">
                            <div className="divBlackHeaderText">
                                Album
                            </div>
                        </div>
                        <div className="flexParent">

                            <div className="flex-child-right2">

                                <Grid container className="divBottomFooter">
                                    <Grid item xs={12} style={{ fontSize: 16, marginBottom: 10 }}>
                                        <label htmlFor="artistName"><div className="flexParent"><div className="flex-li-left">Artist:</div>
                                            <div className="flex-li-right">
                                                <label className="form-control" style={{ fontSize: 17 }} id="artistName" defaultValue=''>{artistName}</label>
                                            </div>
                                        </div></label>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <label htmlFor="albumName"><div className="flexParent"><div className="flex-li-left">Album:</div>
                                            <div className="flex-li-right">
                                                <input className="form-control" type="text" id="albumName" onChange={(e) => setAlbumName(e.target.value)} />
                                                <button id="getArtistMetaDataBtn" type="button" className="bottomButtons bottomButtonAdjust" onClick={getAlbumMetaData}>Get Information</button>
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

export default AlbumEdit

