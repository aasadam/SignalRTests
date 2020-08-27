import React, { Component } from 'react';
import { handleGetWeather } from "../../actions/Weather";
import * as signalR from "@microsoft/signalr";

import { Button, Input } from "reactstrap";

import functions from '../../Util/functions'



const USER_TOKEN = functions.GetCookie('TOKEN');
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/api/WeatherNowByCity",
        {
            accessTokenFactory: () => USER_TOKEN
        })
    .configureLogging(signalR.LogLevel.Information)
    .build();


class WeatherNow extends Component {
    state = {
        weather: "",
        text: "",
        city: ""
    };

    async componentDidMount() {
        hubConnection.start()
            .then(() => {
                hubConnection.on("ChangeWeather", weatherServer => {
                    this.setState({ weather: weatherServer });
                });
            })
            .catch(err => {
                console.log(err);
                this.setState({ weather: "ERROR" });
                if (err.statusCode == 401)
                    this.setState({ weather: "Unautorized" });
            });
    }

    ChangeWeatherServer = e => {
        e.preventDefault();
        hubConnection.invoke("ChangeWeather", this.state.text,);
    }

    render() {
        return (
            <div>
                <Input
                    type="text"
                    name="text"
                    id="text"
                    placeholder="text"
                    onChange={e => this.setState({ text: e.target.value })}
                />
                <Input
                    type="text"
                    name="city"
                    id="city"
                    placeholder="city"
                    onChange={e => this.setState({ city: e.target.value })}
                />
                <Button onClick={(e) => this.ChangeWeatherServer(e)}>Change Weather</Button>
                <br />
                <br />
                <br />
                <p>The Weather is {this.state.weather}</p>
            </div>)
    }
}

export default WeatherNow;