import { useState, useEffect } from "react";
import axios from "axios";
import useInterval from "./useInterval";

export default (url, delay, baseResponse = null) => {
  let [response, setResponse] = useState(baseResponse);
  let [isFetching, setIsFetching] = useState(false);

  const fetch = async () => {
    if (!isFetching) {
      setIsFetching(true);
      const { data } = await axios.get(url);
      setResponse(data);
      setIsFetching(false);
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
