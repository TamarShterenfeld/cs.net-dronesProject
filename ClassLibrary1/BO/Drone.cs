﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IBL.BO
{
    public class Drone
    {
        int id;

        public int Id
        {
            set
            {
                if (value < 0)
                {
                    throw new OverloadException("Id must contain a positive number");
                }
                id = value;
            }
            get { return id; }
        }

        //there's nothing to check for a model - it can hold chars and also digits.
        public string Model { get; set; }

        public WeightCategories MaxWeight { set; get; }

        private static Random rand = new Random();

        private double battery = rand.NextDouble()*20 + 20;        
        public double Battery { get { return battery; } set { battery = value; } }

        private DroneStatuses status { get; set; }
        public DroneStatuses Status { get; set; }

        public ParcelInPassing Parcel { set; get; }

        public Location MyLocation { get; set; }


        /// <summary>
        /// override ToString function.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"id: {Id} \n" +
                   $"model: {Model} \n" +
                   $"maxWeight:  {MaxWeight}\n" +
                   $"battery: {Battery} \n" +
                   $"status: {Status} \n" +
                   $"parcel: {Parcel} \n" +
                   $"myLocation: {MyLocation} \n";
        }
    }
}

