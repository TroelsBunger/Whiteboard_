import React from "react";
import { Route, Switch } from "react-router-dom";
import Welcome from "./Components/Welcome.js"
import WhiteBoard from "./Components/WhiteBoard.js";
import Login from "./Components/LoginForm.js";

export default function Routes() {
  return (
    <Switch>
      <Route path="/" exact component={WhiteBoard}/>
      <Route path ="/login" component={Login}/>
      <Route path ="/welcome" component={Welcome}/>
    </Switch>
  );
}