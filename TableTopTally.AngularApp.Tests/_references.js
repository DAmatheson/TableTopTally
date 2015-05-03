/// <autosync enabled="true" />

/// <reference path="~/_jasmine/IntelliSense/jasmine.d.ts"/>

/// <reference path="~/_jasmine/jasmine.js" />
/// <reference path="~/_jasmine/jasmine-html.js" />
/// <reference path="~/_jasmine/boot.js" />
/// <reference path="~/_jasmine/console.js" />
/// <reference path="~/_jasmine/Helpers/jasmine-helpers.js"/>

/// <reference path="~/../TableTopTally/scripts/library/modernizr-2.7.2.js" />
/// <reference path="~/../TableTopTally/scripts/library/jquery/jquery-2.1.1.js" />
/// <reference path="~/../TableTopTally/scripts/library/angular/angular.js" />
/// <reference path="~/../TableTopTally/scripts/library/angular/angular-resource.js" />
/// <reference path="~/../TableTopTally/scripts/library/angular/angular-route.js" />
/// <reference path="~/../TableTopTally/scripts/library/angular/angular-mocks.js" />
/// <reference path="~/../TableTopTally/scripts/library/angular/angular-messages.js" />
/// <reference path="~/../TableTopTally/scripts/library/angularloadingbar/loading-bar.js" />

/// <reference path="~/../TableTopTally/AngularApp/app.js" />
/// <reference path="~/../TableTopTally/AngularApp/Common/Services/ValuesService.js" />
/// <reference path="~/../TableTopTally/AngularApp/Common/Services/tempRedirectionDataService.js" />
/// <reference path="~/../TableTopTally/AngularApp/Common/Services/layoutValuesService.js" />
/// <reference path="~/../TableTopTally/AngularApp/Common/Directives/ttAppVersionDirective.js" />
/// <reference path="~/../TableTopTally/AngularApp/Common/Directives/ttSubmitDirective.js" />
/// <reference path="~/../TableTopTally/AngularApp/Common/Directives/lessOrEqualDirective.js" />
/// <reference path="~/../TableTopTally/AngularApp/Common/Directives/ttRandomPicker/ttrandomPickerDirective.js" />
/// <reference path="~/../TableTopTally/AngularApp/Common/Directives/ttApiSuccessDisplay/ttApiSuccessDisplayDirective.js" />
/// <reference path="~/../TableTopTally/AngularApp/Common/Directives/ttApiErrorDisplay/ttApiErrorDisplayDirective.js" />
/// <reference path="~/../TableTopTally/AngularApp/Layout/layout.js" />
/// <reference path="~/../TableTopTally/AngularApp/Layout/layoutController.js" />
/// <reference path="~/../TableTopTally/AngularApp/NotFound/notFoundController.js" />
/// <reference path="~/../TableTopTally/AngularApp/Home/home.js" />
/// <reference path="~/../TableTopTally/AngularApp/Home/homeRoutes.js" />
/// <reference path="~/../TableTopTally/AngularApp/Home/homeController.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/games.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/gamesRoutes.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/Services/GameDataService.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/Directives/GameFormFieldsDirective.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/Directives/GameDetailsDirective.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/Controllers/UpdateGameController.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/Controllers/GameListController.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/Controllers/GameDetailController.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/Controllers/DeleteGameController.js" />
/// <reference path="~/../TableTopTally/AngularApp/Games/Controllers/CreateGameController.js" />

// home must be referenced before homeRoutes or homeController etc.
// This is true for all sub modules. The module must be defined before things accessing it are referenced