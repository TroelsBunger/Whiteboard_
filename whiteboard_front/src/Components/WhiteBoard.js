import React, { useState, useContext, useEffect } from "react";
import { url } from '../Config';
import { useHistory } from "react-router-dom";
import { ApplicationContext } from "../App";
import PostCard from "./PostCard";
import './WhiteBoard.css';

export default function WhiteBoard() {
  const [application, setApplication] = useContext(ApplicationContext);
  const [posts, setPosts] = useState([]);

  const history = useHistory();
  const axios = require('axios');
  const getPpostsUrl = url() + '/posts';

  if (!application.LoggedIn) history.push("/login");

  console.log('loggedIn: ', application.LoggedIn);

  useEffect(() => {

    const header = {
      headers: {
        Authorization: 'Bearer ' + application.Token
      }
    };

    axios.get(getPpostsUrl, header)
      .then(function (response)  {
        setPosts(response.data)
      })
  }, []);

  const postWall = () =>
    posts.map( post =>
        <PostCard post={post}/>
      );

  return (
    <div className="postWall">
      {postWall()}
      </div>
  );
}