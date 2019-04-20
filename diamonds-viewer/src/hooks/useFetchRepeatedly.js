import { useState, useEffect } from "react";
import axios from "axios";
import useInterval from "./useInterval";

export default (url, delay, initialValue = null) => {
  let [response, setResponse] = useState(initialValue);

  const fetch = async () => {
    const { data } = await axios.get(url);
    setResponse(data);
  };

  useEffect(() => {
    fetch();
  }, []);

  useInterval(() => {
    fetch();
  }, delay);

  return response;
};
