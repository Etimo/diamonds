import styled from "styled-components";

export default styled.div`
  border: 1px solid #707070;
  width: ${props => `${props.smallCellSize}vw`};
  height: ${props => `${props.smallCellSize}vw`};
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;

  @media only screen and (max-width: 900px) {
    width: ${props => `${props.bigCellSize}vw`};
    height: ${props => `${props.bigCellSize}vw`};
  }
`;
