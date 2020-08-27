import React, { Component } from 'react';

import { handleAuthenticate } from "../../actions/User";
import { Button, FormGroup, Label, Input, Row, Col, Form } from "reactstrap";

class Login extends Component {
    state = {
        username: ""
    };

    FormLogin = e => {
        e.preventDefault();
        handleAuthenticate(this.state.username);
    }

    render() {
        return (
            <Form onSubmit={e => this.FormLogin(e)}>
                <FormGroup>
                    <Label for="UserName">UserName</Label>
                    <Input
                        type="text"
                        name="UserName"
                        id="UserName"
                        placeholder="UserName"
                        onChange={e => this.setState({ username: e.target.value })}
                    />
                </FormGroup>

                <Row>
                    <Col md="12" className="text-center">
                        <Button className="botao" type="submit">
                            Log In
                        </Button>
                    </Col>
                </Row>
            </Form>
        );
    }
}

export default Login;