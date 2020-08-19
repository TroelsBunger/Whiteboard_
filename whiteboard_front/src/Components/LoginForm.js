import React, { useState, useContext } from "react";
import { Button, FormGroup, FormControl, FormLabel } from "react-bootstrap";
import { useHistory } from "react-router-dom";
import { url } from '../Config';
import { ApplicationContext } from '../App';
import "./LoginForm.css";

export default function Login() {
  const [application, setApplication ] = useContext(ApplicationContext);
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [loginMessage, setLoginMessage] = useState("");

  const history = useHistory();

  function validateForm() {
    return userName.length > 0 && password.length > 0;
  }

  function handleSubmit(event) {

    const axios = require('axios');

    const route = url() + '/login?username=' + userName + '&password=' + password;


    axios.get(route)
      .then(function (response){
        //success
        setLoginMessage("Logged in successfully");


        setApplication((prevState) => ({
          ...prevState,
          LoggedIn: true,
          Username: userName,
          UserId: response.data.user.id,
          Token: response.data.token,
          NoOfPosts : response.data.user.noOfPosts
        }));

        //localStorage.setItem('context', JSON.stringify(application));

        history.push("/welcome");

      })

      .catch(function (error){
        console.log('error', error);
        setLoginMessage("Incorrect user name and/or password given");
      });

    console.log('button pressed');
    event.preventDefault();
  }

  return (
    <div className="LoginPage">
      <span className="WelcomeText">Welcome to whiteboard login!</span>
    <div className="Login">
      <form onSubmit={handleSubmit}>
        <FormGroup controlId="username" bsSize="large">
          <FormLabel>User name</FormLabel>
          <FormControl
            autoFocus
            type="username"
            value={userName}
            onChange={e => setUserName(e.target.value)}
          />
        </FormGroup>
        <FormGroup controlId="password" bsSize="large">
          <FormLabel>Password</FormLabel>
          <FormControl
            value={password}
            onChange={e => setPassword(e.target.value)}
            type="password"
          />
        </FormGroup>
        <Button block bsSize="large" disabled={!validateForm()} type="submit">
          Login
        </Button>
        <span>{loginMessage}</span>
      </form>
    </div>
    </div>
  );
}
