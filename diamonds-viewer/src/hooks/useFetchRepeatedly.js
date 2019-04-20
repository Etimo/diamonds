import { useState, useEffect } from "react";
import axios from "axios";
import useInterval from "./useInterval";

export default (url, delay, baseResponse = null) => {
  let [response, setResponse] = useState(baseResponse);
  let [isFetching, setFetching] = useState(false);

  const fetch = async () => {
    if (!isFetching) {
      setFetching(true);
      const { data } = await axios.get(url);
      setResponse(data);
      setFetching(false);
    }
  };

  useEffect(() => {
    fetch();
  }, []);

  useInterval(() => {
    fetch();
  }, delay);

  return response;
};
