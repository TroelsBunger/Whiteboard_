import React, { useState, useContext } from "react";
import {Button, FormControl, FormGroup, FormLabel} from 'react-bootstrap';
import { useHistory } from "react-router-dom";
import { url } from '../Config';
import { ApplicationContext } from "../App";
import './Welcome.css';

export default function Welcome() {
  const [application, setApplication] = useContext(ApplicationContext);
  const [postContent, setPostContent] = useState("");
  const [postMessage, setPostMessage] = useState("");

  const history = useHistory();
  const axios = require('axios');
  const route = url() + '/posts';

  if (!application.LoggedIn) history.push("/login");

  console.log('loggedIn: ', application.LoggedIn);

  console.log('postMessage:', postMessage);

  function handleWhiteBoardButton() {
    history.push("/");
  }

  function handleNewPost(){

    const data = {
      Content: postContent,
      User:{
        Id: application.UserId
      }
    };

    axios.post(route, data, {
      headers:{
        Authorization: 'Bearer ' + application.Token,
      }
    })
      .then(function(response){
        setPostMessage("Post created successfully!");
      })
    }

  function validateForm() {
    return postContent.length > 0;
  }

  function signOut(){
    localStorage.clear();
    history.push('/login');
  }


  return (
    <div className="welcome">
      <div>
        <h1>Welcome {application.Username}</h1>
        <h3>Here are your options: </h3>
        <div className="welcomeContent">
          <form className="postCreate" onSubmit={handleNewPost}>
            <FormGroup className="button" controlId="username" bsSize="large">
              <FormLabel>Create a new post: </FormLabel>
              <FormControl
                autoFocus
                type="content"
                value={postContent}
                onChange={e => setPostContent(e.target.value)}
              />
            </FormGroup>
            <Button className="button" block bsSize="large" disabled={!validateForm()} type="submit">
              Create
            </Button>
            <span>{postMessage}</span>
          </form>
          <Button className="button" onClick={handleWhiteBoardButton}>View WhiteBoard</Button>
          <Button className="button" onClick={signOut}>Sign out </Button>
        </div>
      </div>
    </div>
  );
}