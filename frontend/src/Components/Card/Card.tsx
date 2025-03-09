import React, { SyntheticEvent } from "react";
import "./Card.css";
import { CompanySearch } from "../../company";
import AddPortfolio from "../Portfolio/AddPortfolio/AddPortfolio";

interface Props {
  id: string;
  searchResult: CompanySearch;
  onPortfolioCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({
  id,
  searchResult,
  onPortfolioCreate,
}: Props) => {
  return (
    <div
      className="flex flex-row items-center justify-between w-full p-6 bg-slate-100 rounded-lg gap-x-6 md:flex-wrap"
      key={id}
      id={id}
    >
      <h2 className="font-bold text-center text-black">
        {searchResult.name} ({searchResult.symbol})
      </h2>
      <p className="text-black">{searchResult.currency}</p>
      <p className="font-bold text-black">
        {searchResult.exchange} - {searchResult.exchangeFullName}
      </p>
      <AddPortfolio
        onPortfolioCreate={onPortfolioCreate}
        symbol={searchResult.symbol}
      />
    </div>
  );
};

export default Card;
