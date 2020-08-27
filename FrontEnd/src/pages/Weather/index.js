import React, { Component } from 'react';
import { handleGetWeather } from "../../actions/Weather";

import { Button, FormGroup, Label, Input, Row, Col, Form } from "reactstrap";

class Weather extends Component {
    state = {
        weathers: []
    };

    async componentDidMount() {
        var t = await handleGetWeather();
        this.setState({ weathers: t });

    }

    render() {
        const lines = this.state.weathers.map(function(weather) {
            return <tr>
                <td>{weather.summary}</td>
        </tr>
        });

        return (
            <table>
                <tr>
                    <th>Wheather</th>
                </tr>
                {lines}
            </table>
        );
    }
}

export default Weather;