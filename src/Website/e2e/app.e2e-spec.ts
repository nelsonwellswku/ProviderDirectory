import { Website2Page } from './app.po';

describe('website2 App', () => {
  let page: Website2Page;

  beforeEach(() => {
    page = new Website2Page();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
