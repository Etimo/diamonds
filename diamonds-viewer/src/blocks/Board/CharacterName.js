import styled from "styled-components";

export default styled.p`
  text-overflow: ellipsis;
  white-space: nowrap;
  text-overflow: ellipsis;
  text-align: center;
  font-size: 70%;
  line-height: 1;
  background: rgba(255, 255, 255, 0.8);
  border-radius: 2px;
  padding-bottom: 2px;
  margin: 0;

  @media only screen and (max-width: 900px) {
    font-size: 50%;
  }
`;
