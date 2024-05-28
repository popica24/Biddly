import { useEffect, useState } from "react";

type Props = {
  endsAt: string | undefined;
};

const Counter = (props: Props) => {
  const calculateTimeLeft = () => {
    const endDate = new Date(props.endsAt!).getTime();
    const currentTime = new Date().getTime();
    const difference = endDate - currentTime;

    return difference > 0
      ? {
          hours: Math.floor(difference / (1000 * 60 * 60)),
          minutes: Math.floor((difference / 1000 / 60) % 60),
          seconds: Math.floor((difference / 1000) % 60),
        }
      : {
          hours: 0,
          minutes: 0,
          seconds: 0,
        };
  };
  useEffect(() => {
    const timer = setInterval(() => {
      const newTimeLeft = calculateTimeLeft();
      setTimeLeft(newTimeLeft);

      if (
        newTimeLeft.hours === 0 &&
        newTimeLeft.minutes === 0 &&
        newTimeLeft.seconds === 0
      ) {
        setTimerExpired(true);
        clearInterval(timer);
      }
    }, 1000);

    // Check if timer should be expired immediately on component mount/update
    if (
      timeLeft.hours === 0 &&
      timeLeft.minutes === 0 &&
      timeLeft.seconds === 0
    ) {
      setTimerExpired(true);
    }

    return () => clearInterval(timer);
  }, [props.endsAt]);

  const [timeLeft, setTimeLeft] = useState(calculateTimeLeft());
  const [timerExpired, setTimerExpired] = useState(false);
  if (timerExpired) {
    return <></>;
  }
  return (
    <div className="flex flex-row items-center justify-center mt-6">
      <div className="border border-[#ffed4b] flex flex-col items-center justify-center px-8 aspect-[4/3] rounded-xl">
        <div className="text-3xl font-medium">{timeLeft.hours}</div>
        <div className="text-xs text-[#8d8d8d]">Hours</div>
      </div>
      <span className="mx-4 text-4xl">:</span>
      <div className="border border-[#ffed4b] flex flex-col items-center justify-center px-8 aspect-[4/3] rounded-xl">
        <div className="text-3xl font-medium">{timeLeft.minutes}</div>
        <div className="text-xs text-[#8d8d8d]">Minutes</div>
      </div>
      <span className="mx-4 text-4xl">:</span>
      <div className="border border-[#ffed4b] flex flex-col items-center justify-center px-8 aspect-[4/3] rounded-xl">
        <div className="text-3xl font-medium">{timeLeft.seconds}</div>
        <div className="text-xs text-[#8d8d8d]">Seconds</div>
      </div>
    </div>
  );
};

export default Counter;
