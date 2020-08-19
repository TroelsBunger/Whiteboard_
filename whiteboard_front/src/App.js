import React, {useState, useEffect} from 'react';
import LoginForm from './Components/LoginForm';
import { AppContext } from "./libs/contextLib";
import Routes from "./Routes";
import './App.css';

const initialState = {
  LoggedIn: false,
  Username: "",
  UserId: -1,
  Token: "Initial token"
};

export const ApplicationContext = React.createContext([initialState, () => {}]);


export default function App() {

  const user = JSON.parse(window.localStorage.getItem('context')) || initialState;

  const [application, setApplication] = useState(user);

  console.log('app:', application);

  useEffect(() =>{
    localStorage.setItem('context', JSON.stringify(application))
  }, [application]);


  return (
    <div className="App">
      <ApplicationContext.Provider value={[application, setApplication]}>
      <Routes/>
      </ApplicationContext.Provider>
    </div>
  );
}

