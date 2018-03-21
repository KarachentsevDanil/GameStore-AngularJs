import * as resources from "../../../resources/resources";

const baseGameUrl = resources.urls.baseUrl + "api/game/";
const baseCategoryUrl = resources.urls.baseUrl + "api/category/";
const baseRateUrl = resources.urls.baseUrl + "api/rate/";

export const gameUrls = {
    getGamesUrl: baseGameUrl + "GetGamesByParams",
    getGameByIdUrl: baseGameUrl + "GetGameById",
    getRecomendedGamesByIdUrl: baseGameUrl + "GetRecomendedGames",
    addGameUrl: baseGameUrl + "CreateGame",
    editGameUrl: baseGameUrl + "EditGame"
};

export const categoryUrls = {
    getCategoriesUrl: baseCategoryUrl + "GetCategories",
    addCategoryUrl: baseCategoryUrl + "AddCategory"
};

export const rateUrls = {
    getGameRatesByIdUrl: baseRateUrl + "GetGameRates",
    addRateToGameUrl: baseRateUrl + "CreateFeed"
};

export const categoryLabels = {
    headers: {
        addCategoryLabel: "Add Category"
    },
    properties: {
        categoryNameLabel: "Name"
    },
    commands: {
        addCategoryLabel: "Add",
        closeLabel: "Close"
    },
    validationMessages: {
        categoryNameRequired: "Name is required."
    }
};

export const gameLabels = {
    headers: {
        addGameLabel: "Add Game",
        editGameLabel: "Edit Game",
        filtersLabel: "Filters",
        gamesLabel: "Games",
        recomendedGamesLabel: "Recomended to buy with"
    },
    properties: {
        uploadMainPhotoLabel: "Upload main photo:",
        uploadIconPhotoLabel: "Upload icon photo:",
        uploadPhotosLabel: "Upload photos for game galery:",
        uploadGameFileLabel: "Upload game file for download:",
        updateMainPhotoLabel: "Update main photo",
        updateIconPhotoLabel: "Update icon photo",
        uploadExtraPhotosLabel: "Upload extra photos for game galery:",
        updateGameFileLabel: "Update game file for download:",

        gameNameLabel: "Name",
        gameCategoryLabel: "Category",
        gamePriceLabel: "Price",
        gameDescriptionLabel: "Description",

        currentPhotosLabel: "Current photo's:",
        mainPhotoLabel: "Main:",
        iconPhotoLabel: "Icon:",

        filterSortLabel: "Sort",
        filterTitleLabel: "Title",
        filterCategoriesLabel: "Categories",
        fitlerTitleSearchLabel: "Search ...",
        filterPriceRangeLabel: "Price Range",

        gameNumberLabel: "Game Id #",
        gameOutputNameLabel: "Name:",
        gameOutputCategoryLabel: "Category:",
        gameOutputPriceLabel: "Price:",
        gameOutputDescriptionLabel: "Description:",
        gameOutputRatingLabel: "Rating:",
        gameFeedbacksCountLabel: "Count Of Feedback's:",
        gameGalleryLabel: "Trailer's and Screenshot's",
        gameMoney: "USD"
    },
    commands: {
        addGameLabel: "Add Game",
        addCategoryLabel: "Add Category",
        updateGameLabel: "Update Game",
        buyGameLabel: "Buy",
        gameDetailsLabel: "Details",
        downloadGameLabel: "Download",
        applyFilterLabel: "Apply",
        clearFilterLabel: "Clear"
    },
    validationMessages: {
        gameNameRequiredMessage: "Name is required.",
        gamePriceRequiredMessage: "Price is required.",
        gameDescriptionRequiredMessage: "Description is required."
    }
};

export const rateLabels = {
    headers: {
        commetnsLabel: "Comments"
    },
    properties: {
        commentCustomerLabel: "Customer:",
        commentMessageLabel: "Comment",
        commentRatingLabel: "Rating:",
        commentCreatedOnLabel: "Created On:"
    },
    commands: {
        addCommentLabel: "Add Comment"
    },
    validationMessages: {
        commentMessageRequiredMessage: "Message is required."
    }
};

export const popupMessages = {
    commentAdded: "Comment was successfully added.",
    commentAddedError: "Error occure when adding comment.",
    categoryCreatedMessage: "Category was successfylly added.",
    gameAddedMessage: "Game was successfylly added.",
    gameAddedErrorMessage: "Error occure while adding game.",
    gameUpdatedMessage: "Game was successfylly updated.",
    gameUpdatedErrorMessage: "Error occure while updating game."
};
