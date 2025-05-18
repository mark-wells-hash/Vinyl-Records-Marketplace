import { FC, useState, useEffect } from 'react'
import { DataGrid, GridColDef } from '@mui/x-data-grid'
import { CircularProgress, Tooltip, Grid } from '@mui/material';
import Box from '@mui/material/Box';
import configData from "../config.json";
import { Link } from 'react-router-dom';
import { getUser } from './CommonFunctions';
import LiveSearch from "./LiveSearch";

const columns: GridColDef[] = [

    //{
    //    field: 'albumId', headerName: 'Actions', headerClassName: 'header', width: 90, disableColumnMenu: true,
    //    sortable: false, filterable: false, renderCell: (params) => (
    //        <>
    //            <Link to={`/albumDetail/${params.value}`} ><img src="../editInfo.png" alt="Album Details" title="Edit" className="iconSize" /></Link>
    //            <a href={`/form/${params.value}`}> <img src="../delete.png" alt="Album Delete" title="Delete" className="iconSize" /></a >
    //        </>
    //    )
    //}, icons8-no-image-64
    {
        field: 'thumbnailUrl', headerName: 'Actions', headerClassName: 'header', width: 149, disableColumnMenu: true,
        sortable: false, filterable: false, renderCell: (params) => { 
            if(params.value === null) {
                return <Link to = {`/albumDetail/${params.row.albumId}`}> <img src="icons8-no-image-64.png" alt="Album Details" title="Edit" className="iconSizeMedium" /></Link >;
            }
            return <Link to={`/albumDetail/${params.row.albumId}`}><img src={params.value} alt="Album Details" title="Edit" className="iconSizeMedium" /></Link>; 
        }
    },
    {
        field: 'albumName', headerName: 'Record', headerClassName: 'header', flex: 3,
        sortable: false, filterable: false, renderCell: (params) => (
            <>
                <Grid container className="divBottomFooter">
                    <Grid item xs={12}>
                        <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Record:</div> <div className="flex-li-right">{params.row.albumName}</div></div></label>
                    </Grid>
                    <Grid item xs={12}>
                        <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Label:</div> <div className="flex-li-right">{params.row.label}</div></div></label>
                    </Grid>
                    <Grid item xs={12}>
                        <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Released:</div> <div className="flex-li-right">{params.row.releaseDate}</div></div></label>
                    </Grid>
                    <Grid item xs={12}>
                        <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Format:</div> <div className="flex-li-right">{params.row.mediaTypeString}</div></div></label>
                    </Grid>
                    <Grid item xs={12}>

                        <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Condition:</div> <div className="flex-li-right"><Tooltip title={params.row.conditionDescription}><span className="">{params.row.conditionName} <img src="../icons8-information-64.png" alt="Album Details" className="iconSizeSmallerNoFloat" /></span></Tooltip></div></div></label>
                    </Grid>
                    <Grid item xs={12}>
                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Genre:</div> <div className="flex-li-right">{params.row.genreString}</div></div></label>
                    </Grid>
                    <Grid item xs={12}>
                    <label htmlFor={'user'}><div className="flexParent"><div className="flex-li-left">Style:</div> <div className="flex-li-right">{params.row.styleString}</div></div></label>
                    </Grid>

                </Grid>

            </>
        )
    },
    { field: 'artistName', headerName: 'Artist', headerClassName: 'header', flex: 1 },
    //{ field: 'mediaName', headerName: 'Record Type', headerClassName: 'header', flex: 0.25 },
    //{
    //    field: 'conditionName', headerName: 'Condition', headerClassName: 'header', flex: 1.5, renderCell: (params) => (
    //        <Tooltip title={params.row.conditionDescription} >
    //            <span className="table-cell-trucate">{params.value} <img src="../info.png" alt="Album Details" className="iconSizeSmaller" /></span>
    //        </Tooltip>
    //    )
    //},
    { field: 'description', headerName: 'Description', headerClassName: 'header', flex: 2 },
]

interface Props { }

const Search: FC<Props> = (props): JSX.Element => {
    const user = getUser();
    const [tableData, setTableData] = useState([])
    const [loading, setLoading] = useState(true);
    const [results, setResults] = useState<{ albumId: string; albumName: string; artistId: string; artistName: string }[]>();
    const [selectedArtist, setSelectedArtist] = useState<{
        albumId: string;
        albumName: string;
        artistId: string;
        artistName: string;
    }>();
    const [selectedAlbum, setSelectedAlbum] = useState<{
        albumId: string;
        albumName: string;
        artistId: string;
        artistName: string;
    }>();
    console.log(props);
    type changeHandler = React.ChangeEventHandler<HTMLInputElement>;
    const handleChange: changeHandler = (e) => {
        const { target } = e;
        console.log("handleChange " + target.value)
        if (!target.value.trim()) return setResults([]);

        fetch(configData.SERVER_URL + 'Album/Search/' + target.value.trim())
            .then((data) => data.json())
            .then((data) => {
                setResults(data);
                console.log("Check how many times this gets called " + results);
                setLoading(false);
            });
    };

    const populateSearchResults = ((searchValue: string) => {
        //let searchValue = '';
        //if (selectedArtist) {
        //    searchValue = selectedArtist.artistName;
        //}
        //if (selectedAlbum) {
        //    searchValue = selectedAlbum.albumName;
        //}

        fetch(configData.SERVER_URL + 'Album/Search/' + searchValue)
                .then((data) => data.json())
                .then((data) => {
                    setTableData(data);
                    console.log("Called populate search ");
                    setLoading(false);
                });
    });

    useEffect(() => {
        fetch(configData.SERVER_URL + 'Album/GetAlbumsByStatusId/' + 1)
            .then((data) => data.json())
            .then((data) => {
                setTableData(data);
                //console.log("Check how many times this gets called");
                setLoading(false);
            });

    }, []) //user, tableData, loading
    //console.log("what is selectedProfile at this time: " + selectedProfile.albumName + " : " + + selectedProfile.artistName);
    if (selectedAlbum != null && selectedAlbum.albumId != '') {
        console.log("what is selectedAlbum at this time: " + selectedAlbum.albumName);
        populateSearchResults(selectedAlbum.albumName);
        const newAlbum = {
            albumId: '',
            albumName: '',
            artistId: '',
            artistName: '',
        };
        setSelectedAlbum(newAlbum);
    }
    else if (selectedArtist != null && selectedArtist.artistId != '') {
        console.log("what is selectedArtist at this time: " + selectedArtist.artistName);
        populateSearchResults(selectedArtist.artistName);
        const newAlbum = {
            albumId: '',
            albumName: '',
            artistId: '',
            artistName: '',
        };
        setSelectedArtist(newAlbum);
    }

    //<a href="https://www.freepik.com/icons/information/2#uuid=8c3949a8-d38a-4ddd-9ba3-1efab98e984f">Icon by customicondesign_1</a>
    if (loading) {

        return <CircularProgress style={{ color: "gold" }} className="circular" />;
    }
    else {
        return (
            <div className="mainPanelNoPad">
                <div className="divBlackHeader">
                    <div className="divBlackHeaderText">
                        Search
                    </div>
                </div>
                <div className="flex-child-right2 divBottomFooter">
                    <LiveSearch
                        results={results}
                        value={selectedAlbum?.albumName}
                        renderItem={(item) =>
                            <p>{item.artistName}</p>}
                        renderItem2={(item) =>
                            <p>{item.albumName}</p>}
                        onChange={handleChange}
                        onSelectA={(item) => setSelectedArtist(item)}
                        onSelectB={(item) => setSelectedAlbum(item)}
                        placeholder = "Search albums and albums by artists"
                        widthIn="370px"
                        heightIn="30px"
                    />
                </div>
            <div className="gridPanel">

                <Box sx={{ '& .MuiDataGrid-root': { border: 0 } }} style={{ borderRadius: 5, top: 100 }}>
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
                                color: 'black',
                            },
                            '& .MuiDataGrid-main': {
                                borderRadius: 2,
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
                </div>
            </div>
        )
    }
}

export default Search

