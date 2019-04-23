import { useState } from "react";
import useInterval from "./useInterval";
import axios from "axios";

export default (url, delay, baseResponse = null) => {
  let [response, setResponse] = useState(baseResponse);
  let [isFetching, setIsFetching] = useState(false);

  useInterval(() => {
    const fetch = async () => {
      if (!isFetching) {
        setIsFetching(true);
        const { data } = await axios.get(url);
        setResponse(data);
        setIsFetching(false);
      }
    };
    fetch();
  }, delay);

  return response;
};
