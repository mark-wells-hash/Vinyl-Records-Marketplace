
export class artistObj {
    artistId: number;
    name: string;
    description: string;
    discogResourceUrl: string;
    thumbnailUrl: string;
    coverImageUrl: string;
    userId: number;
    musicians: unknown;
    constructor() {
        this.artistId = 0;
        this.name = "";
        this.description = "";
        this.discogResourceUrl = "";
        this.thumbnailUrl = "";
        this.coverImageUrl = "";
        this.musicians = [];
        this.userId = 0;
    }
}
export class musicianObj {
    musicianId: number;
    albumId: number;
    artistId: number;
    musicianName: string;
    description: string;
    discogResourceUrl: string;
    userId: number;
    constructor() {
        this.musicianId = 0;
        this.albumId = 0;
        this.artistId = 0;
        this.musicianName = "";
        this.description = "";
        this.discogResourceUrl = "";
        this.userId = 0;
    }
}
export class songObj {
    songId: number;
    albumId: number;
    songName: string;
    description: string;
    userId: number;
    constructor() {
        this.songId = 0;
        this.albumId = 0;
        this.songName = "";
        this.description = "";
        this.userId = 0;
    }
}

export class albumMusicians {
    albumMusiciansId: number;
    albumId: number;
    musicianId: number;
    album: unknown;
    musician: unknown;
    constructor() {
        this.albumMusiciansId = 0;
        this.albumId = 0;
        this.musicianId = 0;
        this.album = {};
        this.musician = {};
        //this.musician = musicianList[0];
    }
}
export class albumGenres {
    albumGenreId: number;
    albumId: number;
    genreId: number;
    album: unknown;
    genre: unknown;
    constructor() {
        this.albumGenreId = 0;
        this.albumId = 0;
        this.genreId = 0;
        this.album = {};
        this.genre = {};
        //this.genre = genreList[0];
    }
}
export class albumMediaTypes {
    albumMediaTypeId: number;
    albumId: number;
    mediaTypeId: number;
    album: unknown;
    mediaType: unknown;
    constructor() {
        this.albumMediaTypeId = 0;
        this.albumId = 0;
        this.mediaTypeId = 0;
        this.album = {};
        this.mediaType = {};
        //this.mediaType = mediaList[0];
    }
}
export class albumStyles {
    albumStyleId: number;
    albumId: number;
    styleId: number;
    album: unknown;
    style: unknown;
    constructor() {
        this.albumStyleId = 0;
        this.albumId = 0;
        this.styleId = 0;
        this.album = {};
        this.style = {};
        //this.style = styleList[0];
    }
}
export class amountWithBreakdown {
    totalCost: number;
    currency: string;
    tax: number;
    shipping: number;

    constructor() {
        this.totalCost = 0.00;
        this.currency = "";
        this.tax = 0.00;
        this.shipping = 0.00;
    }

}
export class items {
    itemName: string;
    itemDescription: string;
    itemCost: number;

    constructor() {
        this.itemName = "";
        this.itemDescription = "";
        this.itemCost = 0;
    }
}
export class payee {
    merchantId: string;
    merchantEmail: string;

    constructor() {
        this.merchantId = "";
        this.merchantEmail = "";
    }

}
export class purchaseUnit {
    referenceId: number;
    customId: number;
    amount: unknown;
    payee: unknown;
    items: items[];

    constructor() {
        this.referenceId = 0;
        this.customId = 0;
        this.amount = {};
        this.payee = {};
        this.items = [];
    }
}
