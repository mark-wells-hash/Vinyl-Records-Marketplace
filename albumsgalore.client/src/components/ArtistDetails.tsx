import { useState, useEffect, MouseEvent } from 'react'
import { useParams } from 'react-router';
import { useNavigate } from "react-router-dom";
import { CircularProgress, Grid } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid'
import Box from '@mui/material/Box';
import { Tooltip } from 'react-tooltip';
import configData from "../config.json";
import { Link } from 'react-router-dom';
import { getUser } from './CommonFunctions';

const columns: GridColDef[] = [

    {
        field: 'albumId', headerName: 'Actions', headerClassName: 'header', width: 90, disableColumnMenu: true,
        sortable: false, filterable: false, renderCell: (params) => (
            <>
                <Link to={`/albumAddEdit/${params.value}/${params.row.artistId}/${params.row.artistName}`} ><img src="../editInfo.png" alt="Album Details" title="Edit" className="iconSize" /></Link>
                <a href={`/form/${params.value}`}> <img src="../delete.png" alt="Album Delete" title="Delete" className="iconSize" /></a >
            </>
        )
    },
    { field: 'albumName', headerName: 'Record', headerClassName: 'header', flex: 1 },
    { field: 'statusName', headerName: 'Status', headerClassName: 'header', flex: 0.75 },
    {
        field: 'conditionName', headerName: 'Condition', headerClassName: 'header', flex: 1.5, renderCell: (params) => (
            <>
                {params.value}
                <a data-tooltip-id={params.row.albumName}><img src="../icons8-information-64.png" alt="Album Details" className="iconSizeSmallerNoFloat" /></a>
                <Tooltip id={params.row.albumName} content={params.row.conditionDescription} />
            </>
        )
    },
    { field: 'description', headerName: 'Description', headerClassName: 'header', flex: 2 },
]


const ArtistDetail = () => {
    //Holds current user
    const user = getUser();
    //Holds record user
    const [userId, setUserId] = useState(0);

    const navigate = useNavigate();
    const { artistId } = useParams();
    const [artist, setArtist] = useState(null);
    const [loading, setLoading] = useState(true);
    const [tableData, setTableData] = useState([])
    const [musicianList, setMusicianList] = useState([])

    useEffect(() => {
        console.log("ALBUM " + artistId);
        fetch(configData.SERVER_URL + 'Artists/ArtistDetails/' + artistId)
            .then((data) => data.json())
            .then((data) => {
                //console.log("Check how many times this gets called in AlbumDetail: " + JSON.stringify(data));
                setArtist(data)
                setUserId(data.userId);
                setMusicianList(data.musicians);
                setLoading(false);
            });

    }, [artistId])

    useEffect(() => {
        fetch(configData.SERVER_URL + 'Album/GetAlbumsByArtistId/' + artistId)
            .then((data) => data.json())
            .then((data) => {
                setTableData(data);
                console.log("Check how many times this gets called");
                setLoading(false);
            });

    }, [])

    const handleNavigate = (event: MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        console.log("Changing page" + artistId)
        navigate('/artistAddEdit/' + artistId)
    }

    if (user == null || user.userId != userId) {
        const element = document.getElementById("bottomButton");
        if (element) {
            element.style.display = 'none';
        }
    }

    if (loading) {

        return <CircularProgress style={{ color: "gold" }} className="circular" />;
    }
    else {
        if (artist != null) {
            return (
                <div className="mainPanelNoPad">
                    <div className="divBlackHeader">
                        <div className="divBlackHeaderText">
                            Artist: {artist['name']}
                        </div>
                    </div>
                    <div className="flexParent">
                        <div style={{ paddingLeft: 10, paddingBottom: 5 }}>
                            <img src={artist['thumbnailUrl']} alt="Album Picture" className="iconSizeBiggerBig"></img>
                        </div>
                        <div className="flex-child-right2">

                            <Grid container className="divBottomFooter">
                                <Grid item xs={12}>
                                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Artist:</div>
                                        <div className="flex-li-right">{artist['name']}</div></div></label>
                                </Grid>
                                <Grid item xs={12}>
                                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Description:</div>
                                        <div className="flex-li-right">{artist['description']}</div></div></label>
                                </Grid>
                            </Grid>

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
                    <div className="gridPanel">

                        <Box sx={{ '& .MuiDataGrid-root': { border: 0 } }} style={{ borderRadius: 10, top: 100 }}>
                            <DataGrid
                                rows={tableData}
                                columns={columns}
                                getRowId={(row) => row.albumId}
                                initialState={{
                                    pagination: { paginationModel: { pageSize: 10 } },
                                }}
                                getRowHeight={() => "auto"}
                                pageSizeOptions={[5, 10, 25]}
                                autoHeight {...tableData}
                                // {...tableData} disableVirtualization
                                sx={{
                                    '& .MuiDataGrid-cell:hover': {
                                        color: 'primary.main',
                                    },
                                    '& .MuiDataGrid-main': {
                                        borderRadius: 2.8,
                                        color: 'white'
                                    },
                                    '& .MuiDataGrid-filler': {
                                        backgroundColor: 'black'
                                    },
                                    "& .MuiDataGrid-sortIcon": {
                                        opacity: 'inherit !important',
                                        color: "#ffc300",
                                    },
                                    "& .MuiDataGrid-menuIconButton": {
                                        opacity: 'inherit !important',
                                        visibility: 'visible',
                                        color: "#ffc300",
                                        width: "auto"
                                    },
                                    '& .MuiDataGrid-iconButtonContainer': {
                                        visibility: 'visible',
                                        width: "auto"
                                    },
                                }}
                            />
                        </Box>
                        <hr></hr>
                        <div className="createDiv" >

                            <div className="createLink">
                                <Link type="button" className="createLink" to={'/albumAddEdit/0/' + artistId + '/' + artist['name']}>Create New Album</Link>
                            </div>
                        </div>
                    </div>
                    <div id="bottomButton">
                        <hr />
                        <div className="createDiv" >
                            <div className="">
                                {/*<Link type="submit" className="createLink" to='/artistEdit/artistId'>Edit Album</Link>*/}
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

export default ArtistDetail;