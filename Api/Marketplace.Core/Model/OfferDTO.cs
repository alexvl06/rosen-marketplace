// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;

namespace Marketplace.Core.Model;

public class OfferDTO
{


    public string CategoryName { get; set; }


    public string Description { get; set; }

    public string Location { get; set; }
    public string PictureUrl { get; set; }

    public DateTime PublishedOn { get; set; } = DateTime.Now;

    public string Title { get; set; }

  public string Username { get; set; }
}