# Bookify
The 2016 Apprenticeship exam Bookify project.

## Introduction

Bookify is an online book library and shop, that lets members buy and borrow epub books, and read them either online, or via the smarphone app,

## What is this?

This is the final result of the apprenticeship exam that started on EUC Syd Sønderborg, on the 4th of August 2016.
The general idea was to make a service that allows users to buy and borrow books online, and for publishers to upload their own books as they get published.

---

## Setting up
*  Backend & API

The API is a ASP.Net Web API, and the Backend is a simple Data Access repository.

    1. Install Visual Studio 2015
        - You will need a ConnectionString and AppSettings config to build.
    2. Build project
    3. Run unit tests to verify everything works
    5. Debug or publish solution.


___

*  Website

The website is a basic Node.JS app.

    1. Install Node.JS & Node Package Manager (NPM)
    2. Open a commandline in %GIT_SOURCE%/Bookify/Bookify.Website/App
    3. Run 'npm i'

    Debug:
    4. Run 'npm start'
    5. Open your webbrowser on http://localhost:8080/

    Production
    4. Run 'npm run build'
    5. Publish the Index.html & Bundle.js that will be generated in the current folder (App)
    6. Define Index.html as default route

___

*  iOS Smartphone App

There really isn't all that much to getting up and running with the smartphone app.

    1. Install Visual Studio 2015
    2. Install Xamarin
    3. Change ApiConfig's Website path to point to the location of your API (localhost?). See API and Backend
    4. Build Code
    5. Run Unit tests to verify it runs
    6. Connect to your Mac machine
    7. Deploy the app to your iOS device/simulator

For device deployment, see [Device Provisioning - Xamarin](https://developer.xamarin.com/guides/ios/getting_started/installation/device_provisioning/)

___

* Android Smartphone App

***Pending***

---

### Authors:
* Bjarke Søgaard
* Andreas Hansen
* Jonas Thorsen
* Rick Boysen

### Credits:

#### Backend:
* [Entity Framework](http://www.asp.net/entity-framework)

#### API:
* [ASP.Net Web API](http://www.asp.net/web-api)

#### Website::
* [React](https://facebook.github.io/react/)
* [React-Router](https://github.com/reactjs/react-router)
* [Babel.JS](https://babeljs.io/)
* [MobX](https://github.com/mobxjs/mobx)
* [Lodash](https://lodash.com/)
* [Bootstrap Material Design](http://fezvrasta.github.io/bootstrap-material-design/)

#### App:
* [Xamarin](https://www.xamarin.com/)
* [Rope.Net](https://github.com/Falgantil/Rope.Net) --- MVVM Binding
* [EpubReader.Net](https://github.com/Falgantil/EpubReader.Net) --- Epub Reader for .Net
* [ACR User Dialogs](https://github.com/aritchie/userdialogs)
* [Akavache](https://github.com/akavache/Akavache)
* [ModernHttpClient](https://github.com/paulcbetts/ModernHttpClient)
* [PCL Storage](https://components.xamarin.com/gettingstarted/pclstorage)
* [Polly](https://github.com/App-vNext/Polly)
* [MonoTouch Dialog](https://github.com/migueldeicaza/MonoTouch.Dialog)
