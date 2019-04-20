import { useState, useEffect } from "react";
import axios from "axios";
import useInterval from "./useInterval";

export default (url, delay, initialValue) => {
  let [data, setData] = useState(initialValue);

  const fetch = async () => {
    const { data } = await axios.get(url);
    setData(data);
  };

  useEffect(() => {
    fetch();
  }, []);

  useInterval(() => {
    fetch();
  }, delay);

  return data;
};
