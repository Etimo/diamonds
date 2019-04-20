import { useState, useEffect } from "react";
import axios from "axios";
import useInterval from "./useInterval";

export default (url, delay, initialValue) => {
  let [data, setData] = useState(initialValue);

  useEffect(() => {
    const fetch = async () => {
      const { data } = await axios.get(url);
      setData(data);
    };
    fetch();
  }, []);

  useInterval(() => {
    const fetch = async () => {
      const { data } = await axios.get(url);
      setData(data);
    };
    fetch();
  }, delay);

  return data;
};
