import React from 'react';
import { Switch, Route, Redirect } from 'react-router-dom';
import Login from './pages/Login';
import Weather from './pages/Weather';
import WeatherNow from './pages/WeatherNow';
import WeatherNowByCity from './pages/WeatherNowByCity';



export default function Routes() {
    return (
      <Switch>
        <Route exact path="/login" component={Login} />
        <Route exact path="/Weather" component={Weather} />
        <Route exact path="/Weathernow" component={WeatherNow} />
        <Route exact path="/WeatherNowByCity" component={WeatherNowByCity} />
      </Switch>
    );
  }